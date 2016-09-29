using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Radera.Models;
using Radera.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

        
        public ActionResult GetAuctionDetails(int id)
        {
            RaderaContext RC = new RaderaContext();
            Auction auctionDetails = RC.Auctions.Where(a => a.AuctionID == id).FirstOrDefault();


            var serializedData = JsonConvert.SerializeObject(auctionDetails, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return Content(serializedData, "application/json");
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


            RC.Bids.Add(new Bid
            {
                BidAmount = nyBid.BidAmount,
                BidOwner = user,
                Date = DateTime.Now,
                ThisAuction = selectAuction
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
                Date = DateTime.Now,
                CommentMessage = theMessage,
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

        [HttpGet]
        public ActionResult UpploadPicture()
        {
            RaderaContext RC = new RaderaContext();

            var createAuctionViewModel = new CreateAuctionViewModel { Categories = new SelectList(RC.Category.ToList(), "CategoryId", "CategoryName") };
            return View(createAuctionViewModel);
        }

        [HttpPost]
        public ActionResult UpploadPicture(CreateAuctionViewModel model)
        {
            RaderaContext rc = new RaderaContext();

            if (ModelState.IsValid)
            {
                var picture = FileSave(model.File);
                var category = rc.Category.Find(model.CategoryId);
                int userId = (int)Session["userId"];
                var user = rc.Users.Find(userId);

                rc.Auctions.Add(new Auction
                {
                    AuctionOwner = user,
                    Title = model.Title,
                    Description = model.Description,
                    Picture = picture,
                    PriceBuyout = model.PriceBuyout,
                    StartPrice = model.StartPrice,
                    Category = category,
                });

                rc.SaveChanges();

                return RedirectToAction("UpploadPicture");        
            }

            var createAuctionViewModel = new CreateAuctionViewModel { Categories = new SelectList(rc.Category.ToList(), "CategoryId", "CategoryName") };
            return View(createAuctionViewModel);
        }
        public string FileSave(HttpPostedFileBase file)
        {
            var z = Request.Path;
            string extension = Path.GetExtension(file.FileName);
            var fileNames = Path.GetFileName(file.FileName);
            var guid = Guid.NewGuid().ToString(); //Randomizer filnamnet

            var path = Path.Combine(Server.MapPath("~/image"), guid + fileNames); // gets the map where you save your pictures

            string fl = path.Substring(path.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string newpath = split[1];

            string imagepath = "image/" + newpath;

            file.SaveAs(path);

            return imagepath;
        }
    }
}