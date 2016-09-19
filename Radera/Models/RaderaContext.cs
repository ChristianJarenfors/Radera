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
            //modelBuilder.Entity<User>()
            //    .HasOptional(u => u.UserAuctions);

            modelBuilder.Entity<User>()
                .HasMany(b => b.UserBids)
                .WithRequired(u => u.BidOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(c => c.UserComments)
                .WithRequired(u => u.CommentOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Auction>()
                .HasMany(b => b.Bids)
                .WithRequired(a => a.ThisAuction);

            modelBuilder.Entity<Auction>()
                .HasMany(c => c.Comments)
                .WithRequired(a => a.CommentAuction);
            modelBuilder.Entity<Auction>()
                .HasRequired(a => a.AuctionOwner).WithMany(u=>u.UserAuctions)
                .WillCascadeOnDelete(false);

        }
    }
}