namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class privatemessage2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrivateMessages", "isRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivateMessages", "isRead");
        }
    }
}
