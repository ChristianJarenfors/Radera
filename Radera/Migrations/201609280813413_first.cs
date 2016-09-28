namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "Category_CategoryId" });
            AlterColumn("dbo.Auctions", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Auctions", "Category_CategoryId");
            AddForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "Category_CategoryId" });
            AlterColumn("dbo.Auctions", "Category_CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Auctions", "Category_CategoryId");
            AddForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
