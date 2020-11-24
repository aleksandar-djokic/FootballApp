namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamJoinRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamJoinRequests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestInitiator = c.String(),
                        UserId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamJoinRequests", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamJoinRequests", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamJoinRequests", new[] { "TeamId" });
            DropIndex("dbo.TeamJoinRequests", new[] { "UserId" });
            DropTable("dbo.TeamJoinRequests");
        }
    }
}
