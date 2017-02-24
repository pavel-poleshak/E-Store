namespace E_Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "SubCategory_Id", newName: "SubCategoryId");
            RenameIndex(table: "dbo.Products", name: "IX_SubCategory_Id", newName: "IX_SubCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_SubCategoryId", newName: "IX_SubCategory_Id");
            RenameColumn(table: "dbo.Products", name: "SubCategoryId", newName: "SubCategory_Id");
        }
    }
}
