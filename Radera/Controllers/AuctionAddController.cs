using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radera.Models;


namespace Radera.Controllers
{
    public class AuctionAddController : Controller
    {
        // GET: AuctionAdd
        public ActionResult CreateAuction()
        {
            RaderaContext RC = new RaderaContext();

            var selectUser = (from u in RC.Users
                           where u.Username == "Linus" //Test. Replace with logged in user.
                           select u).Single();

            User thisUser = selectUser;

            RC.Auctions.Add( new Auction
            {
                //All data is for test. Replace with User input.
                AuctionOwner = thisUser,
                PriceStart = 100,
                PriceBuyout = 1000, 
                AuctionTitle = "Cykel",
                AuctionDescription = "Röd barncykel",

            });
            RC.SaveChanges();

            return Redirect("/");
        }
    }
}