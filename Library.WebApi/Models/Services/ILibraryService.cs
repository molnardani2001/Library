using Library.Data;

namespace Library.WebApi.Models
{
    public interface ILibraryService
    {
        /// <summary>
        /// Könyvek lekérdezése
        /// </summary>
        IEnumerable<Book> Books { get; }

        /// <summary>
        /// Kötetek lekérdezése
        /// </summary>
        IEnumerable<Volume> Volumes { get; }


        Book? GetBook(Int32 bookId);

        bool UpdateRent(Rent rent);

        bool DeleteVolume(int id);

        Book? CreateBook(Book book);

        Volume? CreateVolume(Volume volume);

        Volume? GetVolume(Int32 volumeId);

        List<Rent> GetActiveAndFutureRents();

        List<Rent> GetRentsToVolume(int volumeId);

        Rent? GetRentById(int id);

    }
}
