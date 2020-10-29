namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamInvites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamInvites",
                c => new
                    {
                        InviteId = c.Int(nullable: false, identity: true),
                        InviterId = c.String(maxLength: 128),
                        InviteeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InviteId)
                .ForeignKey("dbo.AspNetUsers", t => t.InviteeId)
                .ForeignKey("dbo.AspNetUsers", t => t.InviterId)
                .Index(t => t.InviterId)
                .Index(t => t.InviteeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamInvites", "InviterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamInvites", "InviteeId", "dbo.AspNetUsers");
            DropIndex("dbo.TeamInvites", new[] { "InviteeId" });
            DropIndex("dbo.TeamInvites", new[] { "InviterId" });
            DropTable("dbo.TeamInvites");
        }
    }
}
