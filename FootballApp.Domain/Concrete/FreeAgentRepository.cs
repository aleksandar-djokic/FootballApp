using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class FreeAgentRepository : IFreeAgentRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public bool AddAgentToTeam(int agentId, int teamId)
        {
            var result = false;
            var agent = context.FreeAgents.FirstOrDefault(x => x.Id == agentId);
            var request = context.TeamJoinRequests.FirstOrDefault(x => x.UserId == agent.UserId && x.TeamId == teamId && x.RequestInitiator == "User");
            var role = context.TeamRoles.FirstOrDefault(x => x.Name == "Member");
            try
            {
                context.TeamMembers.Add(new TeamMembers { TeamId = teamId, UserId = agent.UserId, RoleId = role.Id });
                context.TeamJoinRequests.Remove(request);
                if (request != null)
                {
                    context.TeamJoinRequests.Remove(request);
                }
                var invite = context.TeamInvites.FirstOrDefault(x => x.InviteeId == agent.UserId && x.TeamId == teamId);
                if (invite != null)
                {
                    context.TeamInvites.Remove(invite);
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

        public bool CreateProfile(string UserId, string Country, string City)
        {
            var result = false;
            try{

                context.FreeAgents.Add(new FreeAgentProfile() { UserId = UserId, Country = Country, City = City, Active = true });
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public void Edit(int Id,string Country, string City, bool Active)
        {
            var freeAgentProfile = context.FreeAgents.FirstOrDefault(x => x.Id == Id);
            freeAgentProfile.Country =Country;
            freeAgentProfile.City = City;
            freeAgentProfile.Active = Active;
            context.SaveChanges();

        }

        public FreeAgentProfile getFreeAgent(string Userid)
        {
            return context.FreeAgents.FirstOrDefault(x => x.UserId == Userid);
        }
        public IEnumerable<FreeAgentProfile> getFreeAgents(int teamId)
        {
            List<FreeAgentProfile> agents = context.FreeAgents.ToList();
            List<FreeAgentProfile> toRemove = new List<FreeAgentProfile>();
            for(int i=0;i<agents.Count;i++)
            {
                var agentId = agents[i].UserId;
                var isMember = context.TeamMembers.FirstOrDefault(x => x.TeamId == teamId && x.UserId == agentId);
                var isRequested = context.TeamJoinRequests.FirstOrDefault(x => x.TeamId == teamId && x.UserId == agentId && x.RequestInitiator == "Team");
                if (isMember != null || isRequested!=null)
                {
                   toRemove.Add(agents[i]);
                }
            }
            foreach(var a in toRemove)
            {
                agents.Remove(a);
            }
            return agents;
        }

        public ApplicationUser getUser(string userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            return user;
        }

        public bool isRequested(int agentId,int teamId)
        {
            var result = false;
            var agent = context.FreeAgents.FirstOrDefault(x => x.Id == agentId);
            var isRequest = context.TeamJoinRequests.FirstOrDefault(x => x.UserId == agent.UserId && x.TeamId==teamId && x.RequestInitiator=="User");
            if (isRequest != null)
            {
                
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public IEnumerable<FreeAgentProfile> SearchFreeAgents(int teamId, string Country, string City)
        {
            List<FreeAgentProfile> agents = context.FreeAgents.ToList();
            List<FreeAgentProfile> toRemove = new List<FreeAgentProfile>();
            if (Country != "Svi")
            {
                
                if (City != "" && City!="Svi")
                {
                    agents = agents.Where(x => x.Country == Country && x.City == City).ToList();
                }
                else
                {
                    agents = agents.Where(x => x.Country == Country).ToList();
                }
            }
            for (int i = 0; i < agents.Count; i++)
            {
                var agentId = agents[i].UserId;
                var isMember = context.TeamMembers.FirstOrDefault(x => x.TeamId == teamId && x.UserId == agentId);
                var isRequested = context.TeamJoinRequests.FirstOrDefault(x => x.TeamId == teamId && x.UserId == agentId && x.RequestInitiator == "Team");
                if (isMember != null || isRequested != null)
                {
                    toRemove.Add(agents[i]);
                }
            }
            foreach (var a in toRemove)
            {
                agents.Remove(a);
            }

            return agents;
        }

        public bool SendRequestToAgent(int agentId,int teamId)
        {
            var result = false;
            var agent = context.FreeAgents.FirstOrDefault(x => x.Id == agentId);

           
            try
            {
                var teamJoinRequest = new TeamJoinRequests { TeamId = teamId, UserId = agent.UserId, RequestInitiator = "Team" };
                context.TeamJoinRequests.Add(teamJoinRequest);
                context.SaveChanges();
                var notification = new TeamRequestNotification { isRead = false, RecieverId = agent.UserId, TeamId = teamId, RequestId = teamJoinRequest.RequestId };
                context.Notifications.Add(notification);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }

           
            return result;
        }
    }
}
