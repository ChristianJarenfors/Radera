using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radera.Models
{
    public class Bid
    {
        public int BidID { get; set; }
        public int BidAmount { get; set; }
        public DateTime Date { get; set; }

        public virtual User BidOwner { get; set; }
        public virtual Auction ThisAuction { get; set; }
    }
}