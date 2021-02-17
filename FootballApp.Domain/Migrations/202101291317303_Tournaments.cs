namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tournaments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfParticipants = c.Int(),
                        NumberOfRounds = c.Int(),
                        CurrentRound = c.Int(),
                        isActive = c.Boolean(nullable: false),
                        WinnerId = c.Int(),
                        RunnerUpId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.RunnerUpId)
                .ForeignKey("dbo.Teams", t => t.WinnerId)
                .Index(t => t.WinnerId)
                .Index(t => t.RunnerUpId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "WinnerId", "dbo.Teams");
            DropForeignKey("dbo.Tournaments", "RunnerUpId", "dbo.Teams");
            DropIndex("dbo.Tournaments", new[] { "RunnerUpId" });
            DropIndex("dbo.Tournaments", new[] { "WinnerId" });
            DropTable("dbo.Tournaments");
        }
    }
}
