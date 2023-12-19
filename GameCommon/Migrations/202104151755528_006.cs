namespace WpfGameApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _006 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KvmModules", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Vendors", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Players", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Racks", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Servers", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Storages", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.NetworkSwitches", "Name", c => c.String(nullable: false, maxLength: 127, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NetworkSwitches", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Storages", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Servers", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Racks", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Players", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.Vendors", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
            AlterColumn("dbo.KvmModules", "Name", c => c.String(maxLength: 127, storeType: "nvarchar"));
        }
    }
}
