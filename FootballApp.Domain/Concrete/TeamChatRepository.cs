using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class TeamChatRepository : ITeamChatRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public ApplicationUser GetUser(string Id)
        {
            return context.Users.FirstOrDefault(x => x.Id == Id);
        }
        public bool AddMessage(string userId,int teamId,string Message,DateTime time)
        {
            var result = false;
            try
            {
                var newMsg = new TeamChatMessage { Message = Message, TeamId = teamId, UserId = userId, Time = time };
                context.TeamMessages.Add(newMsg);
                context.SaveChanges();
                var member = context.TeamMembers.Where(x => x.TeamId == teamId).ToList();
                for (int i = 0; i < member.Count(); i++)
                {
                    var current = member[i];
                    if (current.UserId != userId)
                    {
                        var notification = new TeamChatNotification { isRead = false, RecieverId = current.UserId, TeamId = teamId,TeamMessage=newMsg};
                        context.Notifications.Add(notification);

                    }
                    
                }
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public IEnumerable<TeamChatMessage> GetMessages(int? messageCount,int teamid)
        {
            var count = messageCount == null ? 0 : messageCount;
            var messages = context.TeamMessages.Where(x => x.TeamId == teamid).OrderByDescending(x=>x.Time).Skip(Convert.ToInt32(count)).Take(10);
            return messages;
        }

        public void readNotifications(string userId, int teamId)
        {
            var notifications = context.Notifications.OfType<TeamChatNotification>().Where(x => x.RecieverId == userId && x.TeamId == teamId);
            foreach (var n in notifications)
            {
                n.isRead = true;
            }
            context.SaveChanges();
        }
        public IEnumerable<ApplicationUser> GetTeamMembers(int teamId)
        {
            var query = context.TeamMembers.Where(x => x.TeamId == teamId).ToList();
            List<ApplicationUser> members = new List<ApplicationUser>();
            foreach (var q in query)
            {
                members.Add(q.User);
            }
            return members;

        }
    }
}
