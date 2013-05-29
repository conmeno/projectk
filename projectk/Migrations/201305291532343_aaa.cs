namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "CategoryID" });
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateIndex("dbo.Articles", "CategoryID");
            AddForeignKey("dbo.Articles", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
    }
}
