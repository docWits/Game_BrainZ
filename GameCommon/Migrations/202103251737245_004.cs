namespace WpfGameApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _004 : DbMigration
    {
        public override void Up()
        {
            // https://ru.stackoverflow.com/questions/878159/mysql-code-first-%D0%BD%D0%B5-%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%B0%D0%B5%D1%82-%D0%BC%D0%B8%D0%B3%D1%80%D0%B0%D1%86%D0%B8%D1%8F-%D0%B4%D0%BB%D1%8F-%D0%BA%D0%BB%D0%B0%D1%81%D1%81%D0%BE%D0%B2-%D0%BC%D0%BE%D0%B4%D0%B5%D0%BB%D0%B8-%D1%81-%D0%B7%D0%B0%D0%B2%D0%B8%D1%81%D0%B8%D0%BC%D0%BE%D1%81%D1%82%D1%8F%D0%BC%D0%B8
            AddColumn("dbo.KvmModules", "VendorID", c => c.Guid());
            AddColumn("dbo.Racks", "VendorID", c => c.Guid());
            AddColumn("dbo.Servers", "VendorID", c => c.Guid());
            AddColumn("dbo.Storages", "VendorID", c => c.Guid());
            AddColumn("dbo.NetworkSwitches", "VendorID", c => c.Guid());
            // Здесь были индексы, но они не работали
            AddForeignKey("dbo.KvmModules", "VendorID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.Racks", "VendorID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.Servers", "VendorID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.Storages", "VendorID", "dbo.Vendors", "ID");
            AddForeignKey("dbo.NetworkSwitches", "VendorID", "dbo.Vendors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("NetworkSwitches", "VendorID", "Vendors");
            DropForeignKey("Storages", "VendorID", "Vendors");
            DropForeignKey("Servers", "VendorID", "Vendors");
            DropForeignKey("Racks", "VendorID", "Vendors");
            DropForeignKey("KvmModules", "VendorID", "Vendors");
            // Здесь были индексы, но они не работали
            // А ещё надо было везде убрать 'dbo.'
            DropColumn("NetworkSwitches", "VendorID");
            DropColumn("Storages", "VendorID");
            DropColumn("Servers", "VendorID");
            DropColumn("Racks", "VendorID");
            DropColumn("KvmModules", "VendorID");
        }
    }
}
