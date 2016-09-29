using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Radera.ViewModels
{
    public class CreateAuctionViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Price")]
        public int StartPrice { get; set; }

        [Required]
        [Display(Name = "Buyout Price")]
        public int PriceBuyout { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public SelectList Categories { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}