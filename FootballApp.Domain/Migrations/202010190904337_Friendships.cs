namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friendships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        User1Id = c.String(nullable: false, maxLength: 128),
                        User2Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User1Id, t.User2Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User1Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User2Id)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "User2Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "User1Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friendships", new[] { "User2Id" });
            DropIndex("dbo.Friendships", new[] { "User1Id" });
            DropTable("dbo.Friendships");
        }
    }
}
