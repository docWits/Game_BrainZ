namespace WpfGameApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _005 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Players");
        }
    }
}
