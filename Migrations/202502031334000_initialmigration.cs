namespace TestFisrtProjectNonCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        Auther = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 2000),
                        CategoryId = c.Byte(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.tbl_Category",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Book", "CategoryId", "dbo.tbl_Category");
            DropIndex("dbo.tbl_Book", new[] { "CategoryId" });
            DropTable("dbo.tbl_Category");
            DropTable("dbo.tbl_Book");
        }
    }
}
