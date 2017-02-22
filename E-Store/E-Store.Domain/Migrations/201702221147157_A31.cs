namespace E_Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.SubCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SubCategories", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubCategories", "Description", c => c.String());
            AlterColumn("dbo.SubCategories", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
