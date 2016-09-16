using Radera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radera.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserInlogg(string Username, string Password)
        {
            RaderaContext RC = new RaderaContext();
            User user = new User();

            if (RC.Users.Any(u => u.Username == Username && u.Password == Password))
            {
                user = RC.Users.Where(u => u.Username == Username && u.Password == Password).First();

                Session["isInlogged"] = true;
                Session["firstName"] = user.FirstName;

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session["isInlogged"] = false;

            return RedirectToAction("Index", "Home");
        }

    }
}