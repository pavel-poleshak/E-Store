namespace E_Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            AddColumn("dbo.Products", "SubCategory_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "SubCategory_Id");
            AddForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Category", c => c.String(nullable: false));
            DropForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "SubCategory_Id" });
            DropIndex("dbo.SubCategories", new[] { "Category_Id" });
            DropColumn("dbo.Products", "SubCategory_Id");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
        }
    }
}
