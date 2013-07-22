namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class require : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Name", c => c.String());
        }
    }
}
