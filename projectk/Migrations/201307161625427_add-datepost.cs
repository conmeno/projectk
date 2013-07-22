namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatepost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "DatePost", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "DatePost");
        }
    }
}
