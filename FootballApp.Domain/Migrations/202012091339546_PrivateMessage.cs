namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 250),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ConversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.PrivateMessages", new[] { "ConversationId" });
            DropIndex("dbo.PrivateMessages", new[] { "UserId" });
            DropTable("dbo.PrivateMessages");
        }
    }
}
