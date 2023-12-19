namespace WpfGameApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Site = c.String(maxLength: 255, storeType: "nvarchar"),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vendors");
        }
    }
}
