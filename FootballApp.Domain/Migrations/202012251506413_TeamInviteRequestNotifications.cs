namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamInviteRequestNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamInviteNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TeamInviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamInvites", t => t.TeamInviteId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TeamInviteId);
            
            CreateTable(
                "dbo.TeamRequestNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamJoinRequests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamRequestNotifications", "RequestId", "dbo.TeamJoinRequests");
            DropForeignKey("dbo.TeamRequestNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamInviteNotifications", "TeamInviteId", "dbo.TeamInvites");
            DropForeignKey("dbo.TeamInviteNotifications", "Id", "dbo.TeamNotifications");
            DropIndex("dbo.TeamRequestNotifications", new[] { "RequestId" });
            DropIndex("dbo.TeamRequestNotifications", new[] { "Id" });
            DropIndex("dbo.TeamInviteNotifications", new[] { "TeamInviteId" });
            DropIndex("dbo.TeamInviteNotifications", new[] { "Id" });
            DropTable("dbo.TeamRequestNotifications");
            DropTable("dbo.TeamInviteNotifications");
        }
    }
}
