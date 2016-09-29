namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        AuctionID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Picture = c.String(),
                        StartPrice = c.Int(nullable: false),
                        PriceBuyout = c.Int(nullable: false),
                        AuctionOwner_UserID = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.AuctionID)
                .ForeignKey("dbo.Users", t => t.AuctionOwner_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.AuctionOwner_UserID)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        City = c.String(),
                        Mail = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Address = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidID = c.Int(nullable: false, identity: true),
                        BidAmount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        BidOwner_UserID = c.Int(nullable: false),
                        ThisAuction_AuctionID = c.Int(),
                    })
                .PrimaryKey(t => t.BidID)
                .ForeignKey("dbo.Users", t => t.BidOwner_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.ThisAuction_AuctionID)
                .Index(t => t.BidOwner_UserID)
                .Index(t => t.ThisAuction_AuctionID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CommentMessage = c.String(),
                        CommentOwner_UserID = c.Int(nullable: false),
                        CommentAuction_AuctionID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Users", t => t.CommentOwner_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.CommentAuction_AuctionID)
                .Index(t => t.CommentOwner_UserID)
                .Index(t => t.CommentAuction_AuctionID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CommentAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Bids", "ThisAuction_AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.Comments", "CommentOwner_UserID", "dbo.Users");
            DropForeignKey("dbo.Bids", "BidOwner_UserID", "dbo.Users");
            DropForeignKey("dbo.Auctions", "AuctionOwner_UserID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "CommentAuction_AuctionID" });
            DropIndex("dbo.Comments", new[] { "CommentOwner_UserID" });
            DropIndex("dbo.Bids", new[] { "ThisAuction_AuctionID" });
            DropIndex("dbo.Bids", new[] { "BidOwner_UserID" });
            DropIndex("dbo.Auctions", new[] { "Category_CategoryId" });
            DropIndex("dbo.Auctions", new[] { "AuctionOwner_UserID" });
            DropTable("dbo.Categories");
            DropTable("dbo.Comments");
            DropTable("dbo.Bids");
            DropTable("dbo.Users");
            DropTable("dbo.Auctions");
        }
    }
}
