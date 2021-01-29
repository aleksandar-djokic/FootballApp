namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketMessage20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketMessages", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TicketMessages", "TicketId");
            CreateIndex("dbo.TicketMessages", "UserId");
            AddForeignKey("dbo.TicketMessages", "TicketId", "dbo.SupportTickets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TicketMessages", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketMessages", "TicketId", "dbo.SupportTickets");
            DropIndex("dbo.TicketMessages", new[] { "UserId" });
            DropIndex("dbo.TicketMessages", new[] { "TicketId" });
            AlterColumn("dbo.TicketMessages", "UserId", c => c.Int(nullable: false));
        }
    }
}
