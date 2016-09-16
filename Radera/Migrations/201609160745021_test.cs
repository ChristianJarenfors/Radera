namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "Title", c => c.String());
            AddColumn("dbo.Auctions", "Description", c => c.String());
            AddColumn("dbo.Auctions", "StartPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "isAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Auctions", "AuctionTitle");
            DropColumn("dbo.Auctions", "AuctionDescription");
            DropColumn("dbo.Auctions", "PriceStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "PriceStart", c => c.Int(nullable: false));
            AddColumn("dbo.Auctions", "AuctionDescription", c => c.String());
            AddColumn("dbo.Auctions", "AuctionTitle", c => c.String());
            DropColumn("dbo.Users", "isAdmin");
            DropColumn("dbo.Auctions", "StartPrice");
            DropColumn("dbo.Auctions", "Description");
            DropColumn("dbo.Auctions", "Title");
        }
    }
}
