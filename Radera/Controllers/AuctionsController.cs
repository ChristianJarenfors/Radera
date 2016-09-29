﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        public ActionResult placeBid(Bid nyBid, Auction thisAuction)
        {
            RaderaContext RC = new RaderaContext();
            User user;

            int userId = (int)Session["userId"];
            user = RC.Users.Find(userId);

            var selectAuction = (from x in RC.Auctions
                                 where x.AuctionID == thisAuction.AuctionID
                                 select x).Single();

 
            var highBid = selectAuction.Bids.OrderByDescending(x => x.BidID)
                .ThenByDescending(b => b.BidAmount)
                .Select(g => g.BidAmount).Take(1);

         
            
          if (nyBid.BidAmount > highBid.FirstOrDefault())
            {
                RC.Bids.Add(new Bid
                {
                    BidAmount = nyBid.BidAmount,
                    BidOwner = user,
                    Date = DateTime.Now,
                    ThisAuction = selectAuction
                });

                selectAuction.currBid = nyBid.BidAmount;

                RC.SaveChanges();
            }

            List<Auction> listOfAuctions = RC.Auctions.ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }
        public ActionResult postComment(string theMessage, Auction thisAuction)
        {
            RaderaContext RC = new RaderaContext();
            User user;

            int userId = (int)Session["userId"];
            user = RC.Users.Find(userId);

            var selectAuction = (from x in RC.Auctions
                                 where x.AuctionID == thisAuction.AuctionID
                                 select x).Single();


            RC.Comments.Add(new Comment
            {
                CommentAuction = selectAuction,
                Date =DateTime.Now,
                 CommentMessage=theMessage,
                 CommentOwner = user 
            });

            RC.SaveChanges();


            List<Auction> listOfAuctions = RC.Auctions.ToList();

            var serializedData = JsonConvert.SerializeObject(listOfAuctions, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Content(serializedData, "application/json");
        }
    }
}