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
    }
}
