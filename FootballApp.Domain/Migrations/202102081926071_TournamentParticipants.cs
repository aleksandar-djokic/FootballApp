namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TournamentParticipants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentParticipants",
                c => new
                    {
                        TournamentId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TournamentId, t.TeamId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentParticipants", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentParticipants", "TeamId", "dbo.Teams");
            DropIndex("dbo.TournamentParticipants", new[] { "TeamId" });
            DropIndex("dbo.TournamentParticipants", new[] { "TournamentId" });
            DropTable("dbo.TournamentParticipants");
        }
    }
}
