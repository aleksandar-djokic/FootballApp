namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdminPrivilege = c.Boolean(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamRoles", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamRoles", new[] { "TeamId" });
            DropTable("dbo.TeamRoles");
        }
    }
}
