namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TournamentMatch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        ScoreP1 = c.Int(nullable: false),
                        ScoreP2 = c.Int(nullable: false),
                        isConcluded = c.Boolean(nullable: false),
                        Participant1Id = c.Int(nullable: false),
                        Participant2Id = c.Int(nullable: false),
                        TournamentId = c.Int(nullable: false),
                        WinnerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Participant1Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.Participant2Id, cascadeDelete: false)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.WinnerId)
                .Index(t => t.Participant1Id)
                .Index(t => t.Participant2Id)
                .Index(t => t.TournamentId)
                .Index(t => t.WinnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentMatches", "WinnerId", "dbo.Teams");
            DropForeignKey("dbo.TournamentMatches", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentMatches", "Participant2Id", "dbo.Teams");
            DropForeignKey("dbo.TournamentMatches", "Participant1Id", "dbo.Teams");
            DropIndex("dbo.TournamentMatches", new[] { "WinnerId" });
            DropIndex("dbo.TournamentMatches", new[] { "TournamentId" });
            DropIndex("dbo.TournamentMatches", new[] { "Participant2Id" });
            DropIndex("dbo.TournamentMatches", new[] { "Participant1Id" });
            DropTable("dbo.TournamentMatches");
        }
    }
}
