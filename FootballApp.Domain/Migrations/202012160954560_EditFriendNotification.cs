namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditFriendNotification : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FriendNotifications", name: "FriendRquestId", newName: "FriendRequestId");
            RenameIndex(table: "dbo.FriendNotifications", name: "IX_FriendRquestId", newName: "IX_FriendRequestId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FriendNotifications", name: "IX_FriendRequestId", newName: "IX_FriendRquestId");
            RenameColumn(table: "dbo.FriendNotifications", name: "FriendRequestId", newName: "FriendRquestId");
        }
    }
}
