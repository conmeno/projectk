namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lan2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "CategoryID", c => c.Int(nullable: false));
        }
    }
}
