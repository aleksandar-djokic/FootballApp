namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTeamNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamNotifications", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.TeamNotifications", "TeamId");
            AddForeignKey("dbo.TeamNotifications", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamNotifications", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamNotifications", new[] { "TeamId" });
            DropColumn("dbo.TeamNotifications", "TeamId");
        }
    }
}
