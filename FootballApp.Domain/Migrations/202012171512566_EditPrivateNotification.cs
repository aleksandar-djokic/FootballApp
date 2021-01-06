namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPrivateNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateChatNotifications", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.PrivateChatNotifications", new[] { "ConversationId" });
            AddColumn("dbo.PrivateChatNotifications", "MessageId", c => c.Int(nullable: false));
            CreateIndex("dbo.PrivateChatNotifications", "MessageId");
            AddForeignKey("dbo.PrivateChatNotifications", "MessageId", "dbo.PrivateMessages", "Id", cascadeDelete: true);
            DropColumn("dbo.PrivateChatNotifications", "ConversationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateChatNotifications", "ConversationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PrivateChatNotifications", "MessageId", "dbo.PrivateMessages");
            DropIndex("dbo.PrivateChatNotifications", new[] { "MessageId" });
            DropColumn("dbo.PrivateChatNotifications", "MessageId");
            CreateIndex("dbo.PrivateChatNotifications", "ConversationId");
            AddForeignKey("dbo.PrivateChatNotifications", "ConversationId", "dbo.Conversations", "Id", cascadeDelete: true);
        }
    }
}
