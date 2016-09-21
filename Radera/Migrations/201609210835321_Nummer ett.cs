namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nummerett : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auctions", "AuctionOwner_UserID", "dbo.Users");
            DropForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Bids", "BidOwner_UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "CommentOwner_UserID", "dbo.Users");
            DropIndex("dbo.Bids", new[] { "ThisAuction_AuctionID" });
            DropIndex("dbo.Comments", new[] { "CommentAuction_AuctionID" });
            AlterColumn("dbo.Bids", "ThisAuction_AuctionID", c => c.Int());
            AlterColumn("dbo.Comments", "CommentAuction_AuctionID", c => c.Int());
            CreateIndex("dbo.Bids", "ThisAuction_AuctionID");
            CreateIndex("dbo.Comments", "CommentAuction_AuctionID");
            AddForeignKey("dbo.Auctions", "AuctionOwner_UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions", "AuctionID");
            AddForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions", "AuctionID");
            AddForeignKey("dbo.Bids", "BidOwner_UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "CommentOwner_UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CommentOwner_UserID", "dbo.Users");
            DropForeignKey("dbo.Bids", "BidOwner_UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Auctions", "AuctionOwner_UserID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "CommentAuction_AuctionID" });
            DropIndex("dbo.Bids", new[] { "ThisAuction_AuctionID" });
            AlterColumn("dbo.Comments", "CommentAuction_AuctionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Bids", "ThisAuction_AuctionID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "CommentAuction_AuctionID");
            CreateIndex("dbo.Bids", "ThisAuction_AuctionID");
            AddForeignKey("dbo.Comments", "CommentOwner_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Bids", "BidOwner_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions", "AuctionID", cascadeDelete: true);
            AddForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions", "AuctionID", cascadeDelete: true);
            AddForeignKey("dbo.Auctions", "AuctionOwner_UserID", "dbo.Users", "UserID");
        }
    }
}
