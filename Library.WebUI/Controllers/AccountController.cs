using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.WebUI.Models;
using Library.Domain.Entities;
using Library.Domain.Concrete;
using System.Web.Security;

namespace Library.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    using (EFDbContext db = new EFDbContext())
                    {
                        db.Users.Add(new User { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Password = model.Password, Login = model.Login, RoleID = 2 });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    if(user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login,true);
                        return RedirectToAction("List", "Book");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже загегистрирован");
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                User user = null;
                Role role = null;
                Role inverseRole = null;

                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name || u.Login == User.Identity.Name);
                    if (user != null)
                    {
                        role = db.Roles.FirstOrDefault(r => r.RoleID == user.RoleID);
                        inverseRole = db.Roles.FirstOrDefault(ir => ir.RoleID != role.RoleID);
                    }
                }
                if (role != null)
                {
                    ViewBag.AuthMessage = "To access this page, log in as " + inverseRole.Name.ToUpper() + "!";
                    if (role.Name == "admin")
                    {
                        return RedirectToAction("List", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("List", "Book");
                    }
                    
                }
            }
            else
            {
                ViewBag.AuthMessage = "Welcome, log in please!";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => (u.Email == model.Login || u.Login == model.Login) && u.Password == model.Password);
                }
                
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("List", "Book");
                }
                
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином еще не загегистрирован");
                }
            }
            return View(model);
        }
    }
}