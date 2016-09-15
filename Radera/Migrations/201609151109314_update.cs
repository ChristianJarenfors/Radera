namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "PriceStart", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "PriceStart");
        }
    }
}
