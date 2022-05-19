using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Library.Data;

namespace Library.Web.Models
{
    public enum SortOrder { POPULARITYNUMBER_DESC, POPULARITYNUMBER_ASC, TITLE_DESC, TITLE_ASC }

    public class LibraryService : ILibraryService
    {
        private readonly LibraryContext _context;
        private readonly RentDateValidator _validator;
        private readonly UserManager<Visitor> _userManager;
        public int BooksPerPage => 20;

        public LibraryService(LibraryContext context, UserManager<Visitor> userManager)
        {
            _context = context;
            _userManager = userManager;
            _validator = new RentDateValidator(context);
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

        public Byte[]? GetBookMainImage(Int32 bookId)
        {
            
            return _context.Books
                .Where(book => book.Id == bookId)
                .Select(book => book.SmallCoverImage)
                .FirstOrDefault();
        }

        public Byte[]? GetBookImage(Int32 bookId, Boolean large)
        {
            Byte[]? imageContent = _context.Books
                .Where(book => book.Id == bookId)
                .Select(book => large ? book.NormalCoverImage : book.SmallCoverImage)
                .FirstOrDefault();

            return imageContent;
        }

        public IEnumerable<Book> GetBooksByOrderedAndPaged(string search, SortOrder sortOrder, Int32 page)
        {
            IEnumerable<Book> books = _context.Books;
            switch (sortOrder)
            {
                case SortOrder.POPULARITYNUMBER_DESC:
                    books = books.OrderByDescending(b => b.PopularityNumber);
                    break;
                case SortOrder.POPULARITYNUMBER_ASC:
                    books = books.OrderBy(b => b.PopularityNumber);
                    break;
                case SortOrder.TITLE_DESC:
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case SortOrder.TITLE_ASC:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
            books = books.Where(book => book.Title.Contains(search) || book.Writer.Contains(search))
                         .Skip((page - 1) * BooksPerPage)
                         .Take(BooksPerPage);
            return books;
        }


        public IEnumerable<Book> GetBooksBySearch(string search)
        {
            IEnumerable<Book> books = _context.Books;
            return books.Where(book => book.Title.Contains(search) || book.Writer.Contains(search));
        }

        public RentViewModel? NewRent(Int32 bookId, Int32 volumeId)
        {
            Volume? volume = _context.Volumes
                .Include(v => v.Book)
                .Include(v => v.Rents)
                .FirstOrDefault(v => v.Id == volumeId);

            Book? book = _context.Books.FirstOrDefault(book => book.Id == bookId);

            if (volume == null || book == null)
                return null;

            RentViewModel rent = new RentViewModel
            {
                Volume = volume,
                Book = book
            };

            rent.Start = DateTime.Today;
            rent.End = DateTime.Today + TimeSpan.FromDays(1);

            return rent;
        }

        public async Task<Boolean> SaveRentAsync(Int32 bookId, Int32 volumeId, String username, RentViewModel rent)
        {
            if (rent.Volume == null)
                return false;

            if (!Validator.TryValidateObject(rent, new ValidationContext(rent, null, null), null))
                return false;

            if (_validator.Validate(rent.Start, rent.End, bookId, volumeId) != RentDateError.Valid)
                return false;

            //Visitor? visitor = _context.Visitors.FirstOrDefault(v => v.UserName == username);
            Visitor visitor = await _userManager.FindByNameAsync(username);

            if (visitor == null)
                return false;

            rent.Start += TimeSpan.FromHours(8);
            rent.End += TimeSpan.FromHours(20);


            _context.Rents.Add(new Rent
            {
                VolumeId = rent.Volume.Id,
                VisitorId = visitor.Id,
                Visitor = visitor,
                Start = rent.Start,
                End = rent.End,
                IsActive = false
            });

            Book? book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
                return false;

            book.PopularityNumber++;

            try
            {
                _context.SaveChanges();
            }catch (Exception)
            {
                return false;
            }

            return true;
        }

        public RentDateError ValidateRent(DateTime start, DateTime end, int bookId, int volumeId)
        {
            start += TimeSpan.FromHours(8);
            end += TimeSpan.FromHours(20);
            return _validator.Validate(start, end, bookId, volumeId);
        }

        public Volume? GetVolume(Int32 volumeId)
        {
            return _context.Volumes.
                Include(v => v.Rents)
                .FirstOrDefault(v => v.Id == volumeId);
        }

        public void RefreshIsActiveRent(Rent rent)
        {
            rent.IsActive = rent.Start <= (DateTime.Now + TimeSpan.FromHours(8)) && (DateTime.Now + TimeSpan.FromHours(20)) <= rent.End;
        }

    }

}
