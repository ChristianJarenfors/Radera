namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Auctions", new[] { "auctionowner_UserID" });
            DropIndex("dbo.Bids", new[] { "ThisAuction_auctionid" });
            DropIndex("dbo.Comments", new[] { "CommentAuction_auctionid" });
            CreateIndex("dbo.Auctions", "AuctionOwner_UserID");
            CreateIndex("dbo.Bids", "ThisAuction_AuctionID");
            CreateIndex("dbo.Comments", "CommentAuction_AuctionID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "CommentAuction_AuctionID" });
            DropIndex("dbo.Bids", new[] { "ThisAuction_AuctionID" });
            DropIndex("dbo.Auctions", new[] { "AuctionOwner_UserID" });
            CreateIndex("dbo.Comments", "CommentAuction_auctionid");
            CreateIndex("dbo.Bids", "ThisAuction_auctionid");
            CreateIndex("dbo.Auctions", "auctionowner_UserID");
        }
    }
}
