namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleTeamIdRelationship : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TeamRoles", new[] { "TeamId" });
            AlterColumn("dbo.TeamRoles", "Name", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.TeamRoles", new[] { "Name", "TeamId" }, unique: true, name: "RoleName_TeamId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TeamRoles", "RoleName_TeamId");
            AlterColumn("dbo.TeamRoles", "Name", c => c.String());
            CreateIndex("dbo.TeamRoles", "TeamId");
        }
    }
}
