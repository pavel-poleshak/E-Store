namespace E_Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            AlterColumn("dbo.Products", "SubCategoryId", c => c.Int());
            CreateIndex("dbo.Products", "SubCategoryId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            AlterColumn("dbo.Products", "SubCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "SubCategoryId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "Id", cascadeDelete: true);
        }
    }
}
