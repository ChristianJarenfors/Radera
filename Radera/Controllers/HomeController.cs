using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radera.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LiveAuction()
        {
            return View();
        }

    }
}