using Library.Data;

namespace Library.Web.Models
{
    public enum RentDateError { Valid, Conflict, LengthInvalid, EndInvalid, StartInvalid, None }

    public class RentDateValidator
    {
        private readonly LibraryContext _context;

        public RentDateValidator(LibraryContext context)
        {
            _context = context;
        }

        public RentDateError Validate(DateTime start, DateTime end, Int32 bookId, Int32 volumeId)
        {
            if (end < start)
                return RentDateError.EndInvalid;

            if (start < DateTime.Now)
                return RentDateError.StartInvalid;

            if (end.Day == start.Day)
                return RentDateError.LengthInvalid;

            Book? book = _context.Books.FirstOrDefault(book => book.Id == bookId);

            if (book == null)
                return RentDateError.None;

            if(_context.Rents.Where(rent => rent.Volume.Id == volumeId && rent.Volume.BookId == bookId && rent.End >= start)
                .ToList()
                .Any(rent => rent.IsConflict(start, end)))
                return RentDateError.Conflict;

            return RentDateError.Valid;

        }
    }
}
