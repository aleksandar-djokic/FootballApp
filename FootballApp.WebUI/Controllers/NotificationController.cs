using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        public INotificationRepository notifications;
        public NotificationController(INotificationRepository repository)
        {
            this.notifications = repository;
        }
        public JsonResult GetNotifications()
        {
            var userId = User.Identity.GetUserId();
            
            var friendNotifications =notifications.GetFriendNotifications(userId).ToList();
            var privateChatNotifications =notifications.GetChatNotifications(userId).ToList();
            var teamChatNotifications = notifications.GetTeamChatNotifications(userId).ToList();
            var teamMatchNotifications = notifications.GetTeamMatchNotifications(userId).ToList();
            var teamMemberNotifications = notifications.GetTeamMemberNotifications(userId).ToList();

            var friendNotificationsVM = new List<FriendNotificationViewModel>();
            for(int i = 0; i < friendNotifications.Count(); i++)
            {
                var current = friendNotifications[i];
                var newVM = new FriendNotificationViewModel { NotificationId = current.Id, FriendRequestId = current.FriendRequestId};
                friendNotificationsVM.Add(newVM);
            }

            var privateChatNotificationsVM = new List<ChatNotificationViewModel>();
            for(int i = 0; i < privateChatNotifications.Count(); i++)
            {
                var current = privateChatNotifications[i];
                var newVM = new ChatNotificationViewModel { NotificationId = current.Id, ConversationId = current.Message.ConversationId };
                privateChatNotificationsVM.Add(newVM);

            }

            var teamChatNotificationsVM = new List<TeamChatNotificationViewModel>();
            for (int i = 0; i < teamChatNotifications.Count(); i++)
            {
                var current = teamChatNotifications[i];
                var newVM = new TeamChatNotificationViewModel { NotificationId = current.Id, MessageId = current.TeamMessageId, TeamId = current.TeamId };
                teamChatNotificationsVM.Add(newVM);
            }

            var teamMatchNotificationsVM = new List<TeamMatchNotificationViewModel>();
            for(int i = 0; i < teamMatchNotifications.Count(); i++)
            {
                var current = teamMatchNotifications[i];
                var newVM = new TeamMatchNotificationViewModel { NotificationId = current.Id, MatchId = current.MatchId, TeamId = current.TeamId ,Type=current.Type};
                teamMatchNotificationsVM.Add(newVM);
            }

            var teamMemberNotificationsVM = new List<TeamMemberNotificationViewModel>();
            for (int i = 0; i < teamMemberNotifications.Count(); i++)
            {
                var current = teamMemberNotifications[i];
                var newVM = new TeamMemberNotificationViewModel { NotificationId = current.Id, TeamId = current.TeamId, RequestId = current.JoinRequestId };
                teamMemberNotificationsVM.Add(newVM);
            }
            var teamInviteNotifications = notifications.teamInviteNotifications(userId);

            var teamNotifications = new { teamChatNotifications = teamChatNotificationsVM, teamMatchNotifications = teamMatchNotificationsVM, teamMemberNotifications = teamMemberNotificationsVM,teamInviteNotifications=teamInviteNotifications };
            var result = new { friendNotifications = friendNotificationsVM, privateChatNotifications = privateChatNotificationsVM, teamNotifications = teamNotifications };
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}