using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radera.Models
{
    public class Auction 
    {
        public int AuctionID { get; set; }
        public string Picture { get; set; }
        public string AuctionTitle { get; set; }
        public string AuctionDescription { get; set; }

        public int PriceBuyout { get; set; }
        public int PriceStart { get; set; }

        public virtual User AuctionOwner { get; set; }
        public virtual IList<Bid> Bids { get; set; }

        public virtual IList<Comment> Comments { get; set; }
    }
}