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


        private static int CompareByMostBids(Auction x, Auction y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Bids.Count().CompareTo(y.Bids.Count());

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.Bids.Count().CompareTo(y.Bids.Count());
                    }
                }
            }
        }
    }
}