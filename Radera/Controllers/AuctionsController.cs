using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Radera.Models;
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
        public ActionResult UpploadPicture(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    Auction newPicture = new Auction();
                    RaderaContext context = new RaderaContext();
                    FileSave(file);

                    
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            return View();
        }
        public void FileSave(HttpPostedFileBase file)
        {
            Auction newAuction = new Auction();
            RaderaContext RC = new RaderaContext();
            int userId = (int)Session["userId"];
            User user = new User();
            user = RC.Users.Find(userId);
            try
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your imgfile");
                }
                if (file != null)
                {
                    var z = Request.Path;
                    string extension = Path.GetExtension(file.FileName);
                    var fileNames = Path.GetFileName(file.FileName);
                    var guid = Guid.NewGuid().ToString(); //Randomizer filnamnet

                    var path = Path.Combine(Server.MapPath("~/image"), guid + fileNames); // gets the map where you save your pictures

                    string fl = path.Substring(path.LastIndexOf("\\"));
                    string[] split = fl.Split('\\');
                    string newpath = split[1];

                    string imagepath = "~/image/" + newpath;
                    var title = Request["title"];
                    int startPrice = Convert.ToInt32(Request["startPrice"]);
                    var Buyout = Convert.ToInt32(Request["Buyout"]);
                    var Description = Request["Description"];

                    var auction = RC.Auctions.Add(new Auction
                    {
                        //All data is for test. Replace with User input.
                        AuctionOwner = user,
                        Title = title,
                        StartPrice = startPrice,
                        PriceBuyout = Buyout,
                        Description = Description,
                        Picture = imagepath
                        
                    });
                    file.SaveAs(path);
                    RC.Auctions.Add(auction);
                    RC.SaveChanges();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}