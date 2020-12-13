namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamChat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 250),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamChatMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamChatMessages", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamChatMessages", new[] { "TeamId" });
            DropIndex("dbo.TeamChatMessages", new[] { "UserId" });
            DropTable("dbo.TeamChatMessages");
        }
    }
}
