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
        public DbSet<Category> Category { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(a => a.UserAuctions)
                .WithRequired(u => u.AuctionOwner);

            //Behövs inte eftersom när man plockar bort
            //User så, tas auktionen bort som i sin tur tar bort kommentarer
            //modelBuilder.Entity<User>()
            //    .HasMany(b => b.UserBids)
            //    .WithRequired(u => u.BidOwner);

            //Behövs inte eftersom när man plockar bort
            //User så, tas auktionen bort som i sin tur tar bort kommentarer
            //modelBuilder.Entity<User>()
            //    .HasMany(c => c.UserComments)
            //    .WithRequired(u => u.CommentOwner)
            //    .WillCascadeOnDelete(true);

            modelBuilder.Entity<Auction>()
                .HasMany(b => b.Bids)
                .WithRequired(b => b.ThisAuction)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Auction>()
                .HasMany(c => c.Comments)
                .WithRequired(c => c.CommentAuction)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Category>()
                .HasMany(a => a.Auction);
                //.WithRequired(u => u.Category);

        }
    }
}