using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications(string userId);
        IEnumerable<FriendRequestNotification> GetFriendNotifications(string userId);
        IEnumerable<Notification> GetTeamNotifications(string userId);
        IEnumerable<PrivateChatNotification> GetChatNotifications(string userId);
        IEnumerable<TeamChatNotification> GetTeamChatNotifications ( string userId);
        IEnumerable<TeamMemberNotification> GetTeamMemberNotifications(string userId);
        IEnumerable<TeamMatchNotification> GetTeamMatchNotifications(string userId);
        int teamInviteNotifications(string userId);
    }
}
