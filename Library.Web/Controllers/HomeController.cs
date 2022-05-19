using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Library.Data;
using Library.Web.Models;

namespace Library.Web.Controllers
{


    public class HomeController : BaseController
    {
        /// <summary>
        /// Vezérlő példányosítása
        /// </summary>
        public HomeController(ILibraryService libraryService) : base(libraryService)  {  }

       
        /// <summary>
        /// Főoldal betöltése
        /// </summary>
        /// <param name="search">Keresés paramétere</param>
        /// <param name="sortOrder">Rendezés paramétere</param>
        /// <param name="page">Oldalszám paramétere</param>
        /// <returns>A könyvek kilistázásának nézete</returns>
        public IActionResult Index(string search = "" , SortOrder sortOrder = SortOrder.POPULARITYNUMBER_DESC, Int32 page = 1)
        {
            if (search == null) return RedirectToAction(nameof(Index));
            IEnumerable<Book> books = _libraryService.GetBooksByOrderedAndPaged(search, sortOrder, page);
            ViewBag.SortOrder = sortOrder;
            ViewBag.PageNumber = page;
            ViewBag.Search = search;

            IEnumerable<Book> filteredBooks = _libraryService.GetBooksBySearch(search);

            if(filteredBooks.Count() % _libraryService.BooksPerPage == 0)
            {
                ViewBag.CurrentBooks = filteredBooks.Count() / _libraryService.BooksPerPage;
            }
            else
            {
                ViewBag.CurrentBooks = 1 + filteredBooks.Count() / _libraryService.BooksPerPage;
            }

            return View("Index", books.ToList());
        }

        /// <summary>
        /// Könyv részleteinek nézete.
        /// </summary>
        /// <param name="bookId">Könyv azonosítója</param>
        /// <returns>A könyv részletes nézete.</returns>
        public IActionResult Details(Int32 bookId)
        {
            Book? book = _libraryService.GetBook(bookId);

            if(book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            //foreach(Volume volume in book.Volumes)
            //{
            //    foreach(Rent rent in volume.Rents)
            //    {
            //        _libraryService.RefreshIsActiveRent(rent);
            //    }
            //}

            //oldal címe
            ViewBag.Title = "Könyv részletei: " + book.Title;

            return View("Details", book);
        }

        /// <summary>
        /// Könyv főképének lekérdezése
        /// </summary>
        /// <param name="bookId">Könyv azonosítója</param>
        /// <returns>A könyv képe, alapértelmezett kép</returns>
        public FileResult ImageForBook(Int32 bookId)
        {
            Byte[]? imageContent = _libraryService.GetBookMainImage(bookId);

            if (imageContent == null)
                return File("~/images/NoImage.png", "image/png");

            return File(imageContent, "image/jpg");
        }

        /// <summary>
        /// A könyv egyik képének lekérdezése
        /// </summary>
        /// <param name="bookId">Könyv azonosítója</param>
        /// <param name="large">Nagy méretű kép lekérése</param>
        /// <returns>Az épület egy képe, vagy az alapértelmezett kép.</returns>
        public FileResult Image(Int32 bookId, Boolean large = false)
        {
            Byte[]? imageContent = _libraryService.GetBookImage(bookId, large);

            if (imageContent == null)
                return File("~/images/NoImage.png", "image/png");

            return File(imageContent, "image/jpg");
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}