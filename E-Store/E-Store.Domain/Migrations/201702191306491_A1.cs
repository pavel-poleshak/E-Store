namespace E_Store.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "ShippingDetails_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ShippingDetails_Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
        }
    }
}
