namespace projectk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Source", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Source");
        }
    }
}
