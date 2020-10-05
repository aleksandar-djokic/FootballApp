namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TeamId, t.UserId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamMembers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamMembers", new[] { "UserId" });
            DropIndex("dbo.TeamMembers", new[] { "TeamId" });
            DropTable("dbo.TeamMembers");
        }
    }
}
