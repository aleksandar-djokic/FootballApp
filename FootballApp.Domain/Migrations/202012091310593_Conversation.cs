namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Conversation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User1Id = c.String(maxLength: 128),
                        User2Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User1Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.User2Id, cascadeDelete: false)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "User2Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "User1Id", "dbo.AspNetUsers");
            DropIndex("dbo.Conversations", new[] { "User2Id" });
            DropIndex("dbo.Conversations", new[] { "User1Id" });
            DropTable("dbo.Conversations");
        }
    }
}
