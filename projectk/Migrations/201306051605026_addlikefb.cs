namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlikefb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Like", c => c.Long(nullable: false));
            AddColumn("dbo.Articles", "Comment", c => c.Long(nullable: false));
            AlterColumn("dbo.Articles", "PageView", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "PageView", c => c.Long());
            DropColumn("dbo.Articles", "Comment");
            DropColumn("dbo.Articles", "Like");
        }
    }
}
