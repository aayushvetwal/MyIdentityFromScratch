using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserMgr => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
        public SignInManager<IdentityUser, string> SigninMgr => HttpContext.GetOwinContext().Get<SignInManager<IdentityUser, string>>();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model) {

            var user = new IdentityUser()
            {
                UserName = model.Email,
                PasswordHash = model.Password
            };
            var result = UserMgr.Create<IdentityUser, string>(user);

            if (result.Succeeded)
            {
                return Content("User created");
            }
            AddErrors(result);

            return View(model);
        }




        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        
        #endregion
    }
}