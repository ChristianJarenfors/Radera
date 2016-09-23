using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Radera.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string CommentMessage { get; set; }

        public virtual User CommentOwner { get; set; }
        public virtual Auction CommentAuction { get; set; }

    }
}