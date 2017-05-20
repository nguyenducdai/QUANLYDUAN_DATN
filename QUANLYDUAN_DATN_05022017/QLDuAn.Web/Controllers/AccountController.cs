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
using AutoMapper;
using QLDuAn.Web.Infastructure.Extentions;

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

        [HttpGet]
        public ActionResult ThongTinCanhan()
        {
            var model = _applicationUserManager.FindById(User.Identity.GetUserId());
            var resondata = Mapper.Map<ApplicationUser , ApplicationUserViewModel>(model);
            return View(resondata);
        }


        [HttpGet]
        public ActionResult CapNhatThongTin()
        {
            var model = _applicationUserManager.FindById(User.Identity.GetUserId());
            var resondata = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(model);
            return View(resondata);
        }


        [HttpPost]
        public async Task<ActionResult> CapNhatThongTin(ApplicationUserViewModel appVM)
        {
            var id = User.Identity.GetUserId();
            var model = await _applicationUserManager.FindByIdAsync(id);
            if(appVM.Image == null)
            {
                appVM.Image = model.Image;
            }
            appVM.Id = id;
            appVM.Created_at = model.Created_at;
            appVM.Updatted_at = model.Updatted_at;
            model.UpdateApplicationUser(appVM);
            var result = await _applicationUserManager.UpdateAsync(model);
            if (result.Succeeded)
            {
                ViewBag.Success = "Cập nhật thành công";
                return RedirectToAction("CapNhatThongTin");

            }
            else
            {
                ViewBag.Success = "Có lỗi sảy ra";
                return RedirectToAction("CapNhatThongTin");

            }

        }


        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


    }
}