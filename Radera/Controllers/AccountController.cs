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
                return View();

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
                return View();
            }

            ///Connect to database to find user

        }
        public ActionResult Login()
        {
            string userName = Request["username"];
            string userPassword = Request["password"];

            using (RaderaContext ct = new RaderaContext())
            {
                var findUser = ct.Users.Where(x => x.Username.ToLower() == userName.ToLower());

                if (findUser.Count() == 1 && findUser.First().Password == userPassword)
                {
                    // sets the session
                    Session["currentUserId"] = findUser.First().UserID;
                    Session["currentUsername"] = findUser.First().Username;
                    Session["loginStatus"] = true;

                    return Redirect("/Home/Index");
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            //resets the session value
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["loginStatus"] = false;

            return Redirect("/Account/Register");
        }
    }
}