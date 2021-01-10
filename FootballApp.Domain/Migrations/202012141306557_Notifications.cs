namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isRead = c.Boolean(nullable: false),
                        RecieverId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecieverId)
                .Index(t => t.RecieverId);
            
            CreateTable(
                "dbo.FriendNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FriendRquestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.FriendshipRequests", t => t.FriendRquestId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.FriendRquestId);
            
            CreateTable(
                "dbo.PrivateChatNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.TeamNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.PrivateChatNotifications", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.PrivateChatNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.FriendNotifications", "FriendRquestId", "dbo.FriendshipRequests");
            DropForeignKey("dbo.FriendNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "RecieverId", "dbo.AspNetUsers");
            DropIndex("dbo.TeamNotifications", new[] { "Id" });
            DropIndex("dbo.PrivateChatNotifications", new[] { "ConversationId" });
            DropIndex("dbo.PrivateChatNotifications", new[] { "Id" });
            DropIndex("dbo.FriendNotifications", new[] { "FriendRquestId" });
            DropIndex("dbo.FriendNotifications", new[] { "Id" });
            DropIndex("dbo.Notifications", new[] { "RecieverId" });
            DropTable("dbo.TeamNotifications");
            DropTable("dbo.PrivateChatNotifications");
            DropTable("dbo.FriendNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
