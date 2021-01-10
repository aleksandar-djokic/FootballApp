namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamNotificationsSpecialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamChatNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TeamMessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamChatMessages", t => t.TeamMessageId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TeamMessageId);
            
            CreateTable(
                "dbo.TeamMatchNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                        MatchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.MatchId);
            
            CreateTable(
                "dbo.TeamMemberNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        JoinRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamJoinRequests", t => t.JoinRequestId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.JoinRequestId);
            
            DropColumn("dbo.TeamNotifications", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamNotifications", "Type", c => c.String());
            DropForeignKey("dbo.TeamMemberNotifications", "JoinRequestId", "dbo.TeamJoinRequests");
            DropForeignKey("dbo.TeamMemberNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamMatchNotifications", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.TeamMatchNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamChatNotifications", "TeamMessageId", "dbo.TeamChatMessages");
            DropForeignKey("dbo.TeamChatNotifications", "Id", "dbo.TeamNotifications");
            DropIndex("dbo.TeamMemberNotifications", new[] { "JoinRequestId" });
            DropIndex("dbo.TeamMemberNotifications", new[] { "Id" });
            DropIndex("dbo.TeamMatchNotifications", new[] { "MatchId" });
            DropIndex("dbo.TeamMatchNotifications", new[] { "Id" });
            DropIndex("dbo.TeamChatNotifications", new[] { "TeamMessageId" });
            DropIndex("dbo.TeamChatNotifications", new[] { "Id" });
            DropTable("dbo.TeamMemberNotifications");
            DropTable("dbo.TeamMatchNotifications");
            DropTable("dbo.TeamChatNotifications");
        }
    }
}
