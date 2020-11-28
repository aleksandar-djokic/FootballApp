namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchesEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "Adress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "Adress");
        }
    }
}
