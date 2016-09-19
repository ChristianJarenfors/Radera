using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Radera.Models
{
    public class RaderaContext: DbContext
    {
        public RaderaContext(): base("RaderaDB")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(a => a.UserAuctions)
                .WithRequired(u => u.AuctionOwner);

            modelBuilder.Entity<User>()
                .HasMany(b => b.UserBids)
                .WithRequired(u => u.BidOwner);

            modelBuilder.Entity<User>()
                .HasMany(c => c.UserComments)
                .WithRequired(u => u.CommentOwner);

            modelBuilder.Entity<Auction>()
                .HasMany(b => b.Bids);

            modelBuilder.Entity<Auction>()
                .HasMany(c => c.Comments);

        }
    }
}