using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDuAn.Web.Models;
using static QLDuAn.Web.App_Start.IdentityConfig;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using QLDuAn.Model.Models;

namespace QLDuAn.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _applicationSingInManager;
        private ApplicationUserManager _applicationUserManager;

        public AccountController(ApplicationSignInManager applicationSingInManage, ApplicationUserManager applicationUserManager)
        {
            this.SignInManager = applicationSingInManage;
            this.UserManager = applicationUserManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _applicationSingInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _applicationSingInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _applicationUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _applicationUserManager = value;
            }
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVM, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _applicationUserManager.Find(loginVM.userName, loginVM.password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties pro = new AuthenticationProperties();
                    pro.IsPersistent = loginVM.remember;
                    pro.RedirectUri = returnUrl;
                    authenticationManager.SignIn(pro,identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);

                    }
                    {
                        return RedirectToAction("Site", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản và mật khẩu không chính xác");
                }
            }

            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            IAuthenticationManager authentication = HttpContext.GetOwinContext().Authentication;
            authentication.SignOut();
            return RedirectToAction("Login","Account");
        }


    }
}