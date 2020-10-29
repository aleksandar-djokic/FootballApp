namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamInviteEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamInvites", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.TeamInvites", "TeamId");
            AddForeignKey("dbo.TeamInvites", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamInvites", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamInvites", new[] { "TeamId" });
            DropColumn("dbo.TeamInvites", "TeamId");
        }
    }
}
