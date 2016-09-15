namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Försökatträddalitedata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "Title", c => c.String());
            AddColumn("dbo.Auctions", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "Description");
            DropColumn("dbo.Auctions", "Title");
        }
    }
}
