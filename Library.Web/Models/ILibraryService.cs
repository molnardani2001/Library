using Library.Data;

namespace Library.Web.Models
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

        int BooksPerPage { get; }


        Book? GetBook(Int32 bookId);

        Byte[]? GetBookMainImage(Int32 bookId);

        Byte[]? GetBookImage(Int32 bookId, Boolean large);

        IEnumerable<Book> GetBooksByOrderedAndPaged(string search, SortOrder sortOrder, Int32 page);

        IEnumerable<Book> GetBooksBySearch(string search);

        RentViewModel? NewRent(Int32 bookId, Int32 volumeId);

        Task<Boolean> SaveRentAsync(Int32 bookId, Int32 volumeId, String username, RentViewModel rent);

        RentDateError ValidateRent(DateTime start, DateTime end, int bookId, int volumeId);

        Volume? GetVolume(Int32 volumeId);

        void RefreshIsActiveRent(Rent rent);
    }
}
