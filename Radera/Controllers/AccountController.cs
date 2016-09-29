using Radera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radera.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User newUser)
        {
            RaderaContext RC = new RaderaContext();

            if (ModelState.IsValid)
            {
                RC.Users.Add(newUser);
                RC.SaveChanges();
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            RaderaContext RC = new RaderaContext();
            User user = new User();

            if (RC.Users.Any(u => u.Username == Username && u.Password == Password && u.isAdmin == true))
            {
                user = RC.Users.Where(u => u.Username == Username).FirstOrDefault();

                Session["isAdmin"] = true;
                Session["firstName"] = user.FirstName;
                Session["userId"] = user.UserID;

                return Redirect("/#/admin");

            }

            if (RC.Users.Any(u => u.Username == Username && u.Password == Password))
            {
                user = RC.Users.Where(u => u.Username == Username && u.Password == Password).First();

                Session["isInlogged"] = true;
                Session["firstName"] = user.FirstName;
                Session["userId"] = user.UserID;

                return Redirect("/#/auctionsinlogged");
            }

            return Redirect("/#/index");            

        }

        public ActionResult Logout()
        {
            //resets the session value
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["isInlogged"] = false;
            Session["isAdmin"] = false;

            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public JsonResult userExist(string username)
        {
            RaderaContext RC = new RaderaContext();
           // var prevUser = RC.Users.Where(p => p.Username == username).FirstOrDefault();

            if (RC.Users.Where(p => p.Username == username).FirstOrDefault() == null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }

        [HttpPost]
        public JsonResult mailExist(string mail)
        {
            RaderaContext RC = new RaderaContext();
            // var prevUser = RC.Users.Where(p => p.Username == username).FirstOrDefault();

            if (RC.Users.Where(p => p.Mail == mail).FirstOrDefault() == null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}