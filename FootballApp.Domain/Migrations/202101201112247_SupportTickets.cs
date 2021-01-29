namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupportTickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Time = c.DateTime(nullable: false),
                        TicketId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupportTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Topic = c.String(),
                        IsOpened = c.Boolean(nullable: false),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupportTickets", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SupportTickets", new[] { "UserId" });
            DropTable("dbo.SupportTickets");
            DropTable("dbo.TicketMessages");
        }
    }
}
