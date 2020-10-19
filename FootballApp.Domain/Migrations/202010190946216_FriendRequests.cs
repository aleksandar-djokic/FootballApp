namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendshipRequests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequesterId = c.String(maxLength: 128),
                        AddresseeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.AddresseeId)
                .ForeignKey("dbo.AspNetUsers", t => t.RequesterId)
                .Index(t => t.RequesterId)
                .Index(t => t.AddresseeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendshipRequests", "RequesterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendshipRequests", "AddresseeId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendshipRequests", new[] { "AddresseeId" });
            DropIndex("dbo.FriendshipRequests", new[] { "RequesterId" });
            DropTable("dbo.FriendshipRequests");
        }
    }
}
