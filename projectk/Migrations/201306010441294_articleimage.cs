namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articleimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Int(nullable: false),
                        LocalURL = c.String(),
                        DropboxURL = c.String(),
                        DropboxShareLink = c.String(),
                        DropboxShareLinkExpire = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArticleImages");
        }
    }
}
