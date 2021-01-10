using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class NotificationRepository : INotificationRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<PrivateChatNotification> GetChatNotifications(string userId)
        {
            var notifications = context.Notifications.OfType<PrivateChatNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            return notifications;
        }

        public IEnumerable<FriendRequestNotification> GetFriendNotifications(string userId)
        {
            var notifications = context.Notifications.OfType<FriendRequestNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            return notifications;
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            var notifications = context.Notifications.Where(x => x.isRead == false && x.RecieverId == userId);
            return notifications;
        }

        public IEnumerable<Notification> GetTeamNotifications(string userId)
        {
            var notifications = context.Notifications.Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            var result = new List<Notification>();
            foreach (var n in notifications)
            {
                if (ObjectContext.GetObjectType(n.GetType()) == typeof(TeamNotification))
                {
                    result.Add(n);
                }
            }
            return result;
        }
        public IEnumerable<TeamChatNotification> GetTeamChatNotifications(string userId)
        {
            var notifications = context.Notifications.OfType<TeamChatNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            
            return notifications;
        }
        public IEnumerable<TeamMemberNotification> GetTeamMemberNotifications(string userId)
        {
            var notifications = context.Notifications.OfType<TeamMemberNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            return notifications;
        }
        public IEnumerable<TeamMatchNotification> GetTeamMatchNotifications(string userId)
        {
            var notifications = context.Notifications.OfType<TeamMatchNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            return notifications;
        }

        public int teamInviteNotifications(string userId)
        {
            var teamInvites = context.Notifications.OfType<TeamInviteNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            var teamRequest = context.Notifications.OfType<TeamRequestNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();

            return teamInvites.Count() + teamRequest.Count();

        }
    }
}
