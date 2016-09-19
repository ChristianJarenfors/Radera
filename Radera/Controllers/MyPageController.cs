using Newtonsoft.Json;
using Radera.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radera.Controllers
{
    public class MyPageController : Controller
    {
        // GET: MyPage
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAuctionsByUserId()
        {
            int userId = (int)Session["userId"];
            RaderaContext RC = new RaderaContext();
            User user = new User();
            user = RC.Users.Find(userId);

            List<Auction> listOfAuctions = RC.Auctions.Where(a => a.AuctionOwner.UserID == user.UserID).ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");

        }

        [HttpPost]
        public ActionResult CreateAuction(Auction newAuction)
        {
            RaderaContext RC = new RaderaContext();

            int userId = (int)Session["userId"];
            User user = new User();
            user = RC.Users.Find(userId);
            
            RC.Auctions.Add(new Auction
            {
                //All data is for test. Replace with User input.
                AuctionOwner = user,
                Title = newAuction.Title,
                StartPrice = newAuction.StartPrice,
                PriceBuyout = newAuction.PriceBuyout
            });
            
            RC.SaveChanges();


            List<Auction> listOfAuctions = RC.Auctions.Where(a => a.AuctionOwner.UserID == user.UserID).ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
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
        public ActionResult UpdateAuction(Auction auction)
        {           
            RaderaContext RC = new RaderaContext();

            int userId = (int)Session["userId"];
            User user = new User();
            user = RC.Users.Find(userId);

            Auction auctionFromRc = RC.Auctions.Where(a => a.AuctionID == auction.AuctionID).FirstOrDefault();

            auctionFromRc.Title = auction.Title;
            auctionFromRc.AuctionOwner.FirstName = auction.AuctionOwner.FirstName;
            auctionFromRc.StartPrice = auction.StartPrice;
            auctionFromRc.PriceBuyout = auction.PriceBuyout;
                        
            RC.SaveChanges();

            List<Auction> listOfAuctions = RC.Auctions.Where(a => a.AuctionOwner.UserID == user.UserID).ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }


        [HttpPost]
        public ActionResult DeleteAuction(int id)
        {
            RaderaContext RC = new RaderaContext();
            Auction auction = new Auction();


            int userId = (int)Session["userId"];
            User user = new User();
            user = RC.Users.Find(userId);


            auction = RC.Auctions.Find(id);

            RC.Auctions.Remove(auction);
            RC.SaveChanges();

            List<Auction> listOfAuctions = RC.Auctions.Where(a => a.AuctionOwner.UserID == user.UserID).ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }

        [HttpPost]
        public ActionResult placeBid(Bid newBid, int auctionID)
        {
            RaderaContext RC = new RaderaContext();
            Auction auction = new Auction();

            int userId = (int)Session["userId"];
            User user;
            user = RC.Users.Find(userId);

            auction = RC.Auctions.Find(auctionID);

            newBid = new Bid();
            newBid.BidOwner = user;
            newBid.ThisAuction = auction;
            newBid.BidAmount = newBid.BidAmount; //Replace with actual bid
            newBid.Date = DateTime.Now;

            auction.Bids.Add(newBid);

            RC.SaveChanges();


            List<Auction> listOfAuctions = RC.Auctions.Where(a => a.AuctionOwner.UserID == user.UserID).ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }

    }
}