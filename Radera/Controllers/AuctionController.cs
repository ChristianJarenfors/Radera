using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radera.Models;

namespace Radera.Controllers
{
    public class AuctionController : Controller
    {
        // GET: Auction
        public ActionResult Index()
        {
            RaderaContext RC = new RaderaContext();
            List<Auction> Auctionlist = RC.Auctions.ToList();
            //Auctionlist.ToList().Sort((x, y) => x.Bids.Count().CompareTo(y.Bids.Count()));
            //Auctionlist.ToList().Sort(CompareByMostBids);
            return View(Auctionlist.GetRange(0,4));
        }
        public ActionResult ExampleAuction()
        {
            RaderaContext RC = new RaderaContext();
            return View(RC.Auctions.Where(a => a.AuctionOwner.Username.ToLower() == "balin").First());
        }

    }
}