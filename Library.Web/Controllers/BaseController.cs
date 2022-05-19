using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Library.Data;

namespace Library.Web.Controllers
{
    public class BaseController : Controller
    {
        // logikát a szolgáltatás osztály mőgé rejtjük
        protected readonly ILibraryService _libraryService;

        public BaseController( ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        /// <summary>
        /// Egy akció meghívása után végrehajtandó metódus
        /// </summary>
        /// <param name="context">Az akció kontextus argumentuma</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            ViewBag.CurrentGuestName = User.Identity?.Name;

            //kiszámoljuk hány oldalon fognak elférni a könyvek
            if (_libraryService.Books.Count() % _libraryService.BooksPerPage == 0)
            {
                ViewBag.MaxPageNumber = _libraryService.Books.Count() / _libraryService.BooksPerPage;
            }
            else
            {
                ViewBag.MaxPageNumber = (_libraryService.Books.Count() / _libraryService.BooksPerPage) + 1;
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
