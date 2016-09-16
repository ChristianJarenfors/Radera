namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "Title", c => c.String());
            AddColumn("dbo.Auctions", "Description", c => c.String());
            AddColumn("dbo.Auctions", "StartPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "isAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isAdmin");
            DropColumn("dbo.Auctions", "StartPrice");
            DropColumn("dbo.Auctions", "Description");
            DropColumn("dbo.Auctions", "Title");
        }
    }
}
