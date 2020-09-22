namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUserModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
        }
    }
}
