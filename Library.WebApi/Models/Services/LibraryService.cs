using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Library.Data;

namespace Library.WebApi.Models
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryContext _context;

        public LibraryService(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> Books => _context.Books;

        public IEnumerable<Volume> Volumes => _context.Volumes;

        public Book? GetBook(Int32 bookId)
        {
            return _context.Books
                .Include(book => book.Volumes)
                .ThenInclude(volume => volume.Rents)
                .FirstOrDefault(book => book.Id == bookId);
        }


        public Volume? GetVolume(Int32 volumeId)
        {
            return _context.Volumes.
                Include(v => v.Rents)
                .FirstOrDefault(v => v.Id == volumeId);
        }

        public bool UpdateRent(Rent rent)
        {
            var rentsToActualVolume = _context.Rents.Where(r => r.VolumeId == rent.VolumeId && r.Id != rent.Id);

            if (rent.IsActive && rentsToActualVolume.Any(r => r.IsActive))
            {
                throw new DataException("already has active rent");
            }
                
            try
            {
                var rentToUpdate = _context.Rents.First(r => r.Id == rent.Id);
                rentToUpdate.VolumeId = rent.VolumeId;
                rentToUpdate.VisitorId = rent.VisitorId;
                rentToUpdate.Start = rent.Start;
                rentToUpdate.End = rent.End;
                rentToUpdate.IsActive = rent.IsActive;
                //_context.Update(rent);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public Book? CreateBook(Book book)
        {
            if (Books.Where(b => b.ISBNNumber.Equals(book.ISBNNumber) && b.Title.Equals(book.Title) &&
                                 b.Writer.Equals(book.Writer) && b.Year == book.Year).Any())
            {
                throw new Exception();
            }

            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return book;
        }

        public Volume? CreateVolume(Volume volume)
        {
            try
            {
                _context.Add(volume);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return volume;
        }

        public bool DeleteVolume(int id)
        {
            var volume = _context.Volumes.Find(id);
            if (volume is null) return false;

            var rents = _context.Rents.Where(r => r.VolumeId == id);

            if(rents.Any(r => r.IsActive)) 
                throw new Exception();

            try
            {
                foreach (var rent in rents)
                {
                    _context.Remove(rent);
                }

                _context.Remove(volume);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public List<Rent> GetActiveAndFutureRents()
        {
            return _context.Rents.Where(rent => !(rent.End >= DateTime.Now)).ToList();
        }

        public List<Rent> GetRentsToVolume(int volumeId)
        {
            return _context.Rents.Where(rent => rent.VolumeId == volumeId && (rent.IsActive || rent.Start >= DateTime.Today)).ToList();
        }

        public Rent? GetRentById(int rentId)
        {
            return _context.Rents.FirstOrDefault(rent => rent.Id == rentId);
        }
    }

}
