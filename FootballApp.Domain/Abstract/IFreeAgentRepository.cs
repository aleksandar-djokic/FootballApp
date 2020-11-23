using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface IFreeAgentRepository
    {
        FreeAgentProfile getFreeAgent(string Userid);
        bool CreateProfile(string UserId,string Country,string City);
        void Edit(int Id,string Country, string City, bool Active);
        IEnumerable<FreeAgentProfile> getFreeAgents(int teamId);
        ApplicationUser getUser(string userId);
        bool SendRequestToAgent(int agentId, int teamId);
        bool isRequested(int agentId, int teamId);
        bool AddAgentToTeam(int agentId, int teamId);
        IEnumerable<FreeAgentProfile> SearchFreeAgents(int teamId,string Country,string City);
    }
}
