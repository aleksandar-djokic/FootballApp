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
                context.TeamMessages.Add(new TeamChatMessage { Message = Message, TeamId = teamId, UserId = userId, Time = time });
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
    }
}
