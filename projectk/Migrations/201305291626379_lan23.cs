namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lan23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Cat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Cat");
        }
    }
}
