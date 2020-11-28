namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamNameUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Teams", "Name", unique: true, name: "TeamName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", "TeamName");
            AlterColumn("dbo.Teams", "Name", c => c.String());
        }
    }
}
