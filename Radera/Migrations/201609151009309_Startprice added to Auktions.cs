namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartpriceaddedtoAuktions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "StartPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "StartPrice");
        }
    }
}
