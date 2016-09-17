using Newtonsoft.Json;
using Radera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Radera.Controllers
{
    public class AuctionsController : Controller
    {
        // GET: Auctions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AuctionbyID(int id)
        {
            RaderaContext RC = new RaderaContext();
            Auction Auctions = RC.Auctions.Where(a => a.AuctionOwner.UserID == id).First();


            //return Json(listOfAuctions, JsonRequestBehavior.AllowGet);

            var serializedData = JsonConvert.SerializeObject(Auctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
            
        }
        public ActionResult GetAuctions()
        {
            RaderaContext RC = new RaderaContext();
            List<Auction> listOfAuctions = RC.Auctions.ToList();


            //return Json(listOfAuctions, JsonRequestBehavior.AllowGet);

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings() {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            
            return Content(serializedData, "application/json");

        }

        [HttpPost]
        public ActionResult AuctionBid(Bid newBid)
        {
            RaderaContext RC = new RaderaContext();
            RC.Bids.Add(newBid);
            RC.SaveChanges();

            List<Auction> listOfAuctions = RC.Auctions.ToList();

            //return Json(listOfAuctions, JsonRequestBehavior.AllowGet);

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
         public ActionResult EditAuction()
        {
            return View();
        }
            [HttpPost]
         public ActionResult EditAuction(Auction editedAuction)
        {
            RaderaContext RC = new RaderaContext();
            Auction auctionToBeSaved = RC.Auctions.Single(a => a.AuctionID == editedAuction.AuctionID);
            auctionToBeSaved = editedAuction;
            RC.SaveChanges();
            return View()
        }
        }
    }
}