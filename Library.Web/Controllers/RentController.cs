using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Library.Data;

namespace Library.Web.Controllers
{
    /// <summary>
    /// Foglalások vezérlője
    /// </summary>
    [Authorize]
    public class RentController : BaseController
    {
        /// <summary>
        /// Felhasználókezelési szolgáltatás
        /// </summary>
        private readonly UserManager<Visitor> _userManager;

        /// <summary>
        /// Vezérlő példányosítása
        /// </summary>
        public RentController(ILibraryService libraryService, UserManager<Visitor> userManager)
            : base(libraryService) 
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Foglalás (oldal lekérése)
        /// </summary>
        /// <param name="bookId">Könyv azonosítója</param>
        /// <param name="volumeId">Kötet azonosítója</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(Int32 bookId, Int32 volumeId)
        {
            RentViewModel? rent = _libraryService.NewRent(bookId, volumeId);

            if(rent == null)
                return RedirectToAction("Index", "Home");

            return View("Index", rent);
        }
        /// <summary>
        /// Foglalás (adatok beküldése)
        /// </summary>
        /// <param name="bookId">Könyv azonosítója</param>
        /// <param name="volumeId">Kötet azonosítója</param>
        /// <param name="rent">Foglalás adatai</param>
        /// <returns>Foglalás eredmény nézete</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Int32 bookId, Int32 volumeId, RentViewModel rent)
        {
            rent.Book = _libraryService.GetBook(bookId);
            rent.Volume = _libraryService.GetVolume(volumeId);

            //string? username = HttpContext.Session.GetString("user");

            if (rent.Book == null || rent.Volume == null)
                return RedirectToAction("Index", "Home");

            switch(_libraryService.ValidateRent(rent.Start, rent.End, bookId, volumeId))
            {
                case RentDateError.StartInvalid:
                    ModelState.AddModelError("Start",
                        "A kezdés dátuma nem megfelelő (múltba nem nyúlhat a foglalás)!");
                    break;
                case RentDateError.EndInvalid:
                    ModelState.AddModelError("End",
                        "A megadott foglalási idő érvénytelen (a foglalás vége korábban van mint a kezdete!)");
                    break;
                case RentDateError.LengthInvalid:
                    ModelState.AddModelError("End",
                        "A megadott foglalási idő érvénytelen (minimum 1 napot kell foglalni)!");
                    break;
                case RentDateError.Conflict:
                    ModelState.AddModelError("Start",
                        "A megadott időpontban a kötet már foglalt!");
                    break;
            }

            if (!ModelState.IsValid)
                return View("Index", rent);

            Visitor visitor = null;

            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                visitor = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            //if(username != null)
            //{
            //    if (!await _libraryService.SaveRentAsync(bookId, volumeId, username, rent))
            //    {
            //        ModelState.AddModelError("", "A foglalás rögzítése sikertelen, kérem próbálja újra!");
            //        return View("Index", rent);
            //    }
            //}

            //biztos, hogy nem lesz null mert csak azoknak tesszük lehetővé a foglalást akik be vannak jelentkezve
            if(visitor != null && !await _libraryService.SaveRentAsync(bookId,volumeId,visitor.UserName, rent))
            {
                ModelState.AddModelError("", "A foglalás rögzítése sikertelen, kérem próbálja újra!");
                return View("Index", rent);
            }


            return View("Result", rent);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
