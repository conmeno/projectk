namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusername_toArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "UserName");
        }
    }
}
