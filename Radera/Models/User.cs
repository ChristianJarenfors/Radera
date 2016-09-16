using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radera.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Mail { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool isAdmin { get; set; }

        public virtual IList<Auction> UserAuctions { get; set; }
        public virtual IList<Bid> UserBids { get; set; }
        public virtual IList<Comment> UserComments { get; set; }
    }
}