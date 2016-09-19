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
        public ActionResult Register()
        {
            string regUsername = Request["registerUsername"];
            string regEmail = Request["registerEmail"];
            string regPassword = Request["registerPassword"];
            string regFirstName = Request["registerFirstName"];
            string regLastName = Request["registerLastName"];
            string regCity = Request["registerCity"];
            string regAdress = Request["registerAdress"];
            var regPhonenumber = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["registerPhoneNumber"]);
            if (regUsername == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                using (RaderaContext context = new RaderaContext())
                {
                    User regUser = new User()
                    {
                        Username = regUsername,
                        Mail = regEmail,
                        Password = regPassword,
                        FirstName = regFirstName,
                        LastName = regLastName,
                        City = regCity,
                        Address = regAdress,
                        PhoneNumber = regPhonenumber
                    };
                    context.Users.Add(regUser);
                    context.SaveChanges();

                }
                return RedirectToAction("Index", "Home");
            }

            ///Connect to database to find user

        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
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
        public ActionResult Logout()
        {
            //resets the session value
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["isInlogged"] = false;

            return RedirectToAction("Index", "Home");

        }
    }
}