using Newtonsoft.Json;
using Radera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radera.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region CRUD Categories & Auctions

        public ActionResult GetSearchCategories()
        {
            RaderaContext RC = new RaderaContext();
            List<Category> listOfCategory = RC.Category.ToList();


            var serializedData = JsonConvert.SerializeObject(listOfCategory, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return Content(serializedData, "application/json");

        }

        public ActionResult GetCategories()
        {
            RaderaContext RC = new RaderaContext();
            List<Category> listOfCategory = RC.Category.ToList();


            var serializedData = JsonConvert.SerializeObject(listOfCategory, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return Content(serializedData, "application/json");

        }

        public ActionResult GetAuctions()
        {
            RaderaContext RC = new RaderaContext();
            List<Auction> listOfAuctions = RC.Auctions.ToList();


            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return Content(serializedData, "application/json");

        }


        [HttpPost]
        public ActionResult SelectAuctionById(int id)
        {
            RaderaContext RC = new RaderaContext();
            Auction auction = new Auction();
            auction = RC.Auctions.Find(id);

            var serializedData = JsonConvert.SerializeObject(auction, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }


        [HttpPost]
        public ActionResult UpdateAuction(Auction uAuction)
        {
            RaderaContext RC = new RaderaContext();

            Category category;
            category = RC.Category.Where(c => c.CategoryId == uAuction.Category.CategoryId).FirstOrDefault();


            Auction auctionFromRc = RC.Auctions.Where(a => a.AuctionID == uAuction.AuctionID).FirstOrDefault();

            auctionFromRc.Title = uAuction.Title;
            auctionFromRc.AuctionOwner.FirstName = uAuction.AuctionOwner.FirstName;
            auctionFromRc.StartPrice = uAuction.StartPrice;
            auctionFromRc.Category = category;
            auctionFromRc.Description = uAuction.Description;

            RC.SaveChanges();

            return GetAuctions();
        }


        [HttpPost]
        public ActionResult DeleteAuction(int id)
        {
            RaderaContext RC = new RaderaContext();
            Auction auction = new Auction();
            
            auction = RC.Auctions.Find(id);

            RC.Auctions.Remove(auction);
            RC.SaveChanges();

            return GetAuctions();
        }

        #endregion


        #region CRUD Users

        public ActionResult GetUsers()
        {
            RaderaContext RC = new RaderaContext();
            List<User> listOfUsers = RC.Users.ToList();


            var serializedData = JsonConvert.SerializeObject(listOfUsers, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return Content(serializedData, "application/json");

        }

        [HttpPost]
        public ActionResult CreateUser(User newuser)
        {
            RaderaContext RC = new RaderaContext();

            var user = RC.Users.Add(new User
            {
                Username = newuser.Username,
                Password = newuser.Password,
                FirstName = newuser.FirstName,
                LastName = newuser.LastName,
                Gender = newuser.Gender,
                City = newuser.City
            });

            RC.Users.Add(user);

            RC.SaveChanges();

            return GetUsers();
        }

        [HttpPost]
        public ActionResult SelectUserById(int id)
        {
            RaderaContext RC = new RaderaContext();
            User user = new User();
            user = RC.Users.Find(id);

            var serializedData = JsonConvert.SerializeObject(user, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }


        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            RaderaContext RC = new RaderaContext();

            User userFromRc = RC.Users.Where(a => a.UserID == user.UserID).FirstOrDefault();

            userFromRc.Username = user.Username;
            userFromRc.Password = user.Password;
            userFromRc.FirstName = user.FirstName;
            userFromRc.LastName = user.LastName;
            userFromRc.Gender = user.Gender;
            userFromRc.City = user.City;

            RC.SaveChanges();

            return GetUsers();
        }


        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            RaderaContext RC = new RaderaContext();
            User user = new User();

            user = RC.Users.Find(id);

            RC.Users.Remove(user);
            RC.SaveChanges();

            return GetUsers();
        }

        #endregion
    }
}