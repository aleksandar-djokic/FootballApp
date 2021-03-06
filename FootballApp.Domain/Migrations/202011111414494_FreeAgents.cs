namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FreeAgents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreeAgentProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Country = c.String(),
                        City = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreeAgentProfiles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FreeAgentProfiles", new[] { "UserId" });
            DropTable("dbo.FreeAgentProfiles");
        }
    }
}
