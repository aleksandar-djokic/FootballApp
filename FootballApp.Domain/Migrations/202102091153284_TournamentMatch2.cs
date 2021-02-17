namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TournamentMatch2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TournamentMatches", "ScoreP1", c => c.Int());
            AlterColumn("dbo.TournamentMatches", "ScoreP2", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TournamentMatches", "ScoreP2", c => c.Int(nullable: false));
            AlterColumn("dbo.TournamentMatches", "ScoreP1", c => c.Int(nullable: false));
        }
    }
}
