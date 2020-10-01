namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamMemberRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamMembers", "RoleId", c => c.Int(nullable: true));
            CreateIndex("dbo.TeamMembers", "RoleId");
            AddForeignKey("dbo.TeamMembers", "RoleId", "dbo.TeamRoles", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamMembers", "RoleId", "dbo.TeamRoles");
            DropIndex("dbo.TeamMembers", new[] { "RoleId" });
            DropColumn("dbo.TeamMembers", "RoleId");
        }
    }
}
