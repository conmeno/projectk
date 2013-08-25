namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ThumbnailURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ThumbnailURL");
        }
    }
}
