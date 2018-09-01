namespace FBClone.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            DropColumn("dbo.Users", "FirtName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FirtName", c => c.String());
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
