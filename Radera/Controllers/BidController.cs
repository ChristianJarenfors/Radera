using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radera.Models;

namespace Radera.Controllers
{
    public class BidController : Controller
    {
        // GET: Bid
        public ActionResult Index()
        {
            RaderaContext RC = new RaderaContext();

            var selectUser = (from u in RC.Users
                              where u.Username == "Cooly" //Replace with user
                              select u).Single();

            var selectAuction = (from a in RC.Auctions
                                 where a.AuctionID == 1 //Replace with auctionID
                                 select a).Single();

            Bid newBid = new Bid();
            newBid.BidOwner = selectUser;
            newBid.ThisAuction = selectAuction;
            newBid.BidAmount = 500; //Replace with actual bid
            newBid.Date = DateTime.Now;

            selectAuction.Bids.Add(newBid);
           
            RC.SaveChanges();
            

            
       

            return View();
        }
    }
}