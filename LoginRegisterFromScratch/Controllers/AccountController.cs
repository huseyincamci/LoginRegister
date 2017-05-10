using LoginRegisterFromScratch.Attributes;
using LoginRegisterFromScratch.Models;
using LoginRegisterFromScratch.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LoginRegisterFromScratch.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        [Login]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid) return View();
            var userStore = new UserStore<ApplicationUser>(new LoginRegisterDbContex());
            var manager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Age = model.Age,
                Address = model.Address,
                CreatedDateTime = DateTime.Now.ToUniversalTime()
            };

            if (photo?.ContentLength > 0)
            {
                var extension = Path.GetExtension(photo.FileName);
                var guid = Guid.NewGuid();
                var fileName = guid + extension;
                var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                user.Photo = fileName;
                photo.SaveAs(path);
            }

            IdentityResult result = manager.Create(user, model.Password);
            if (result.Succeeded)
            {
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                var authManager = Request.GetOwinContext().Authentication;
                authManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Login]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var userStore = new UserStore<ApplicationUser>(new LoginRegisterDbContex());
            var manager = new UserManager<ApplicationUser>(userStore);

            var user = manager.Find(model.UserName, model.Password);
            if (user == null) return View();

            var authManager = Request.GetOwinContext().Authentication;
            var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}