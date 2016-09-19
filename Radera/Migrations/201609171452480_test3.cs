namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions");
            AddForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions", "AuctionID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions", "AuctionID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions");
            AddForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions", "AuctionID");
            AddForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions", "AuctionID");
        }
    }
}
