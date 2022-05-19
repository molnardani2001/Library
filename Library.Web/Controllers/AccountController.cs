using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Microsoft.AspNetCore.Identity;
using Library.Data;

namespace Library.Web.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Felhasználókezelési szolgáltatás
        /// </summary>
        private readonly UserManager<Visitor> _userManager;
        /// <summary>
        /// Authentikációs szolgáltatás
        /// </summary>
        private readonly SignInManager<Visitor> _signInManager;

        public AccountController(ILibraryService libraryService, UserManager<Visitor> userManager,
            SignInManager<Visitor> signInManager)
            : base(libraryService) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Bejelentkezés
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        /// <summary>
        /// Bejelentkezés
        /// </summary>
        /// <param name="user">Bejelentkezés adatai</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
                return View("Login", user);

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberLogin, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Hibás felhasználónév, vagy jelszó.");
                return View("Login", user);
            }

            return RedirectToAction("Index", "Home");
            //if (!ModelState.IsValid)
            //    return View("Login", user);

            //if (!_accountservice.Login(user))
            //{
            //    ModelState.AddModelError("", "Hibás felhasználónév, vagy jelszó.");
            //    return View("Login", user);
            //}

            //return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Regisztráció
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        /// <summary>
        /// Regisztráció
        /// </summary>
        /// <param name="user">Regisztráció adatok</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", user);
            }

            Visitor visitor = new Visitor
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            var result = await _userManager.CreateAsync(visitor, user.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View("Register", user);
            }

            //if (!_accountservice.Register(visitor))
            //{
            //    ModelState.AddModelError("UserName", "A megadott felhasználónév már létezik.");
            //    return View("Register", visitor);
            //}

            //ViewBag.Information = "A regisztáció sikeres volt. Kérjük, jelentkezzen be.";

            //if(HttpContext.Session.GetString("user") != null)
            //{
            //    HttpContext.Session.Remove("user");
            //}

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Kijelentkezés
        /// </summary>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
