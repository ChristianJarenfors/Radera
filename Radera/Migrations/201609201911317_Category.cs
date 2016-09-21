namespace Radera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Auctions", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Auctions", "Category_CategoryId");
            AddForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "Category_CategoryId" });
            DropColumn("dbo.Auctions", "Category_CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
