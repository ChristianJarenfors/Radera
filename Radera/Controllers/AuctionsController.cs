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
        }
         public ActionResult EditAuction(int id = 0)
        {
            RaderaContext RC = new RaderaContext();
            Auction auction = RC.Auctions.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }
            [HttpPost]
        public ActionResult EditAuction(Auction editedAuction)
        {
            RaderaContext RC = new RaderaContext();
            if (ModelState.IsValid)
            {
                RC.Entry(editedAuction).State = System.Data.Entity.EntityState.Modified;
                RC.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(editedAuction);

        }
        public ActionResult Delete(int? id)
        {
            RaderaContext RC = new RaderaContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auction auction = RC.Auctions.Find(id);
            if (auction == null)
            {
                return HttpNotFound();
            }
            return View(auction);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RaderaContext RC = new RaderaContext();
            Auction movie = RC.Auctions.Find(id);
            RC.Auctions.Remove(movie);
            RC.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}