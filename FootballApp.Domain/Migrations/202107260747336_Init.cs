namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                .ForeignKey("dbo.AspNetUsers", t => t.User1Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User2Id)
                .Index(t => t.User1Id)
                .Index(t => t.User2Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProfilePicture = c.Binary(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FreeAgentProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Country = c.String(),
                        City = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Team1Id = c.Int(nullable: false),
                        Team2Id = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        isAccepted = c.Boolean(nullable: false),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team1Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.Team2Id, cascadeDelete: false)
                .Index(t => t.Team1Id)
                .Index(t => t.Team2Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Picture = c.Binary(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Name, unique: true, name: "TeamName")
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isRead = c.Boolean(nullable: false),
                        RecieverId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecieverId)
                .Index(t => t.RecieverId);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 250),
                        isRead = c.Boolean(nullable: false),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.TeamChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 250),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamInvites",
                c => new
                    {
                        InviteId = c.Int(nullable: false, identity: true),
                        InviterId = c.String(maxLength: 128),
                        InviteeId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InviteId)
                .ForeignKey("dbo.AspNetUsers", t => t.InviteeId)
                .ForeignKey("dbo.AspNetUsers", t => t.InviterId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.InviterId)
                .Index(t => t.InviteeId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamJoinRequests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestInitiator = c.String(),
                        UserId = c.String(maxLength: 128),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.UserId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.TeamRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TeamRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        AdminPrivilege = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_RoleName");
            
            CreateTable(
                "dbo.TicketMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Time = c.DateTime(nullable: false),
                        TicketId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SupportTickets", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TicketId)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.TournamentMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        ScoreP1 = c.Int(),
                        ScoreP2 = c.Int(),
                        isConcluded = c.Boolean(nullable: false),
                        Participant1Id = c.Int(nullable: false),
                        Participant2Id = c.Int(nullable: false),
                        TournamentId = c.Int(nullable: false),
                        WinnerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Participant1Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.Participant2Id, cascadeDelete: false)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.WinnerId)
                .Index(t => t.Participant1Id)
                .Index(t => t.Participant2Id)
                .Index(t => t.TournamentId)
                .Index(t => t.WinnerId);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfParticipants = c.Int(),
                        NumberOfRounds = c.Int(),
                        CurrentRound = c.Int(),
                        isActive = c.Boolean(nullable: false),
                        WinnerId = c.Int(),
                        RunnerUpId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.RunnerUpId)
                .ForeignKey("dbo.Teams", t => t.WinnerId)
                .Index(t => t.WinnerId)
                .Index(t => t.RunnerUpId);
            
            CreateTable(
                "dbo.TournamentParticipants",
                c => new
                    {
                        TournamentId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TournamentId, t.TeamId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.FriendNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FriendRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.FriendshipRequests", t => t.FriendRequestId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.FriendRequestId);
            
            CreateTable(
                "dbo.PrivateChatNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.PrivateMessages", t => t.MessageId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.MessageId);
            
            CreateTable(
                "dbo.TeamNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamChatNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TeamMessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamChatMessages", t => t.TeamMessageId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TeamMessageId);
            
            CreateTable(
                "dbo.TeamInviteNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TeamInviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamInvites", t => t.TeamInviteId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TeamInviteId);
            
            CreateTable(
                "dbo.TeamMatchNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                        MatchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.MatchId);
            
            CreateTable(
                "dbo.TeamMemberNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        JoinRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamJoinRequests", t => t.JoinRequestId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.JoinRequestId);
            
            CreateTable(
                "dbo.TeamRequestNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamNotifications", t => t.Id)
                .ForeignKey("dbo.TeamJoinRequests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamRequestNotifications", "RequestId", "dbo.TeamJoinRequests");
            DropForeignKey("dbo.TeamRequestNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamMemberNotifications", "JoinRequestId", "dbo.TeamJoinRequests");
            DropForeignKey("dbo.TeamMemberNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamMatchNotifications", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.TeamMatchNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamInviteNotifications", "TeamInviteId", "dbo.TeamInvites");
            DropForeignKey("dbo.TeamInviteNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamChatNotifications", "TeamMessageId", "dbo.TeamChatMessages");
            DropForeignKey("dbo.TeamChatNotifications", "Id", "dbo.TeamNotifications");
            DropForeignKey("dbo.TeamNotifications", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.PrivateChatNotifications", "MessageId", "dbo.PrivateMessages");
            DropForeignKey("dbo.PrivateChatNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.FriendNotifications", "FriendRequestId", "dbo.FriendshipRequests");
            DropForeignKey("dbo.FriendNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.TournamentParticipants", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentParticipants", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TournamentMatches", "WinnerId", "dbo.Teams");
            DropForeignKey("dbo.TournamentMatches", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", "WinnerId", "dbo.Teams");
            DropForeignKey("dbo.Tournaments", "RunnerUpId", "dbo.Teams");
            DropForeignKey("dbo.TournamentMatches", "Participant2Id", "dbo.Teams");
            DropForeignKey("dbo.TournamentMatches", "Participant1Id", "dbo.Teams");
            DropForeignKey("dbo.TicketMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketMessages", "TicketId", "dbo.SupportTickets");
            DropForeignKey("dbo.SupportTickets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamMembers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamMembers", "RoleId", "dbo.TeamRoles");
            DropForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TeamJoinRequests", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamJoinRequests", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamInvites", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamInvites", "InviterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamInvites", "InviteeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamChatMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamChatMessages", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.PrivateMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Notifications", "RecieverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matches", "Team2Id", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team1Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "User2Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "User1Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendshipRequests", "RequesterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendshipRequests", "AddresseeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FreeAgentProfiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "User2Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "User1Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TeamRequestNotifications", new[] { "RequestId" });
            DropIndex("dbo.TeamRequestNotifications", new[] { "Id" });
            DropIndex("dbo.TeamMemberNotifications", new[] { "JoinRequestId" });
            DropIndex("dbo.TeamMemberNotifications", new[] { "Id" });
            DropIndex("dbo.TeamMatchNotifications", new[] { "MatchId" });
            DropIndex("dbo.TeamMatchNotifications", new[] { "Id" });
            DropIndex("dbo.TeamInviteNotifications", new[] { "TeamInviteId" });
            DropIndex("dbo.TeamInviteNotifications", new[] { "Id" });
            DropIndex("dbo.TeamChatNotifications", new[] { "TeamMessageId" });
            DropIndex("dbo.TeamChatNotifications", new[] { "Id" });
            DropIndex("dbo.TeamNotifications", new[] { "TeamId" });
            DropIndex("dbo.TeamNotifications", new[] { "Id" });
            DropIndex("dbo.PrivateChatNotifications", new[] { "MessageId" });
            DropIndex("dbo.PrivateChatNotifications", new[] { "Id" });
            DropIndex("dbo.FriendNotifications", new[] { "FriendRequestId" });
            DropIndex("dbo.FriendNotifications", new[] { "Id" });
            DropIndex("dbo.TournamentParticipants", new[] { "TeamId" });
            DropIndex("dbo.TournamentParticipants", new[] { "TournamentId" });
            DropIndex("dbo.Tournaments", new[] { "RunnerUpId" });
            DropIndex("dbo.Tournaments", new[] { "WinnerId" });
            DropIndex("dbo.TournamentMatches", new[] { "WinnerId" });
            DropIndex("dbo.TournamentMatches", new[] { "TournamentId" });
            DropIndex("dbo.TournamentMatches", new[] { "Participant2Id" });
            DropIndex("dbo.TournamentMatches", new[] { "Participant1Id" });
            DropIndex("dbo.SupportTickets", new[] { "UserId" });
            DropIndex("dbo.TicketMessages", new[] { "UserId" });
            DropIndex("dbo.TicketMessages", new[] { "TicketId" });
            DropIndex("dbo.TeamRoles", "IX_RoleName");
            DropIndex("dbo.TeamMembers", new[] { "RoleId" });
            DropIndex("dbo.TeamMembers", new[] { "UserId" });
            DropIndex("dbo.TeamMembers", new[] { "TeamId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TeamJoinRequests", new[] { "TeamId" });
            DropIndex("dbo.TeamJoinRequests", new[] { "UserId" });
            DropIndex("dbo.TeamInvites", new[] { "TeamId" });
            DropIndex("dbo.TeamInvites", new[] { "InviteeId" });
            DropIndex("dbo.TeamInvites", new[] { "InviterId" });
            DropIndex("dbo.TeamChatMessages", new[] { "TeamId" });
            DropIndex("dbo.TeamChatMessages", new[] { "UserId" });
            DropIndex("dbo.PrivateMessages", new[] { "ConversationId" });
            DropIndex("dbo.PrivateMessages", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "RecieverId" });
            DropIndex("dbo.Teams", new[] { "User_Id" });
            DropIndex("dbo.Teams", "TeamName");
            DropIndex("dbo.Matches", new[] { "Team2Id" });
            DropIndex("dbo.Matches", new[] { "Team1Id" });
            DropIndex("dbo.Friendships", new[] { "User2Id" });
            DropIndex("dbo.Friendships", new[] { "User1Id" });
            DropIndex("dbo.FriendshipRequests", new[] { "AddresseeId" });
            DropIndex("dbo.FriendshipRequests", new[] { "RequesterId" });
            DropIndex("dbo.FreeAgentProfiles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Conversations", new[] { "User2Id" });
            DropIndex("dbo.Conversations", new[] { "User1Id" });
            DropTable("dbo.TeamRequestNotifications");
            DropTable("dbo.TeamMemberNotifications");
            DropTable("dbo.TeamMatchNotifications");
            DropTable("dbo.TeamInviteNotifications");
            DropTable("dbo.TeamChatNotifications");
            DropTable("dbo.TeamNotifications");
            DropTable("dbo.PrivateChatNotifications");
            DropTable("dbo.FriendNotifications");
            DropTable("dbo.TournamentParticipants");
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentMatches");
            DropTable("dbo.SupportTickets");
            DropTable("dbo.TicketMessages");
            DropTable("dbo.TeamRoles");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TeamJoinRequests");
            DropTable("dbo.TeamInvites");
            DropTable("dbo.TeamChatMessages");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.Notifications");
            DropTable("dbo.News");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.Friendships");
            DropTable("dbo.FriendshipRequests");
            DropTable("dbo.FreeAgentProfiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Conversations");
        }
    }
}
