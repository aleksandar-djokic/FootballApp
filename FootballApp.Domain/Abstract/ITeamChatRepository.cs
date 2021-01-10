using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface ITeamChatRepository
    {
        ApplicationUser GetUser(string Id);
        bool AddMessage(string userId, int teamId, string Message, DateTime time);
        IEnumerable<TeamChatMessage> GetMessages(int? messageCount,int teamid);
        void readNotifications(string userId, int teamId);
        IEnumerable<ApplicationUser> GetTeamMembers(int teamId);

    }
}
