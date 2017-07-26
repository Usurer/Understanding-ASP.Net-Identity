using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Empty.Identity;
using Empty.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Empty.Controllers
{
    public class LoginController : Controller
    {
        private MyUserManager _userManager;
        private MySignInManager _signInManager;

        public MyUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public MySignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<MySignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public LoginController()
        {
        }

        public LoginController(MyUserManager userManager)
        {
            UserManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(LoginViewModel model, string returnUrl)
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            SignInManager.SignIn(user, false, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new MyUser { Id = model.Email, UserName = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
