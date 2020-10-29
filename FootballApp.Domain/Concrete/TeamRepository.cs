using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class TeamRepository : ITeamRepository
    {
        private ApplicationDbContext context =new ApplicationDbContext();
        
        public IEnumerable<Team> Teams { get { return context.Teams.ToList(); } }

        public void AddMember(string UserId, int TeamId,int RoleId)
        {
            ApplicationUser User = context.Users.First(x => x.Id == UserId);
            Team Team = context.Teams.First(x => x.Id == TeamId);
            TeamRole Role = context.TeamRoles.First(x => x.Id == RoleId);
            TeamMembers newMember = new TeamMembers
            {
                TeamId = TeamId,
                UserId = UserId,
                RoleId = RoleId,
                TeamRole = Role,
                Team=Team,
                User=User
            };
            context.TeamMembers.Add(newMember);
            context.SaveChanges();
        }

        public void AddRole(string Name, int TeamId, bool admin = false)
        {
            Team Team = context.Teams.FirstOrDefault(x => x.Id == TeamId);
            TeamRole newRole = new TeamRole
            {
                Name = Name,
                TeamId = TeamId,
                Team = Team,
                AdminPrivilege = admin
            };
            context.TeamRoles.Add(newRole);
            context.SaveChanges();
        }
        //Creates Team
        public void Create(string Name, string Description, byte[] Image, string user)
        {
            ApplicationUser User = context.Users.First(x => x.Id == user);
            Team team = new Team
            {
                Name = Name,
                Description = Description,
                Picture = Image,
                User = User
            };
            context.Teams.Add(team);
            context.SaveChanges();
            AddRole("Owner", team.Id, true);
            AddRole("Member", team.Id, false);
            AddMember(user, team.Id,GetOwnerRoleId(team.Id));
        }

        public void Edit(int TeamId,string Name, string Description, byte[] Image)
        {
            var team = context.Teams.First(x => x.Id == TeamId);
            team.Name = Name;
            team.Description = Description;
            if(Image!= null)
            {
                team.Picture = Image;
            }
            context.SaveChanges();
        }

        public int GetOwnerRoleId(int TeamId)
        {
            TeamRole TeamRole = context.TeamRoles.FirstOrDefault(x => x.TeamId == TeamId);
            return TeamRole.Id;
        }

        public IEnumerable<Team> GetTeamsByMember(string UserId)
        {
            var teams = (from t in context.Teams
                         join tm in context.TeamMembers
                         on t.Id equals tm.TeamId
                         where tm.UserId == UserId
                         select new
                         {
                             Id = t.Id,
                             Name = t.Name,
                             Description = t.Description,
                             Picture = t.Picture


                         }).ToList().Select(x => new Team {Id=x.Id,Name=x.Name,Description=x.Description,Picture=x.Picture }); 
            return teams;
        }
        public IEnumerable<Team> SearchTeam(string Name)
        {
            var teams = context.Teams.Where(x => x.Name.Contains(Name));
            return teams;
        }

        public Team GetTeamByID(int Id)
        {
            Team team=context.Teams.FirstOrDefault(x=>x.Id==Id);
            return team;
        }

        public bool InviteUser(string InviterId, string InviteeId, int teamId, out string msg)
        {
            var result = false;
            msg = "";
            var team = context.Teams.Where(x => x.Id == teamId).FirstOrDefault();
            var inviter = context.Users.Where(x => x.Id == InviterId).FirstOrDefault();
            var invitee = context.Users.Where(x => x.Id == InviteeId).FirstOrDefault();
            var membership = context.TeamMembers.Where(x => x.TeamId == teamId && x.UserId == InviteeId).FirstOrDefault();
            var invitation = context.TeamInvites.Where(x => x.TeamId == teamId && x.InviteeId == InviteeId).FirstOrDefault();

            try
            {
                if (membership != null)
                {
                    msg = "User is already in that team.";
                }
                else if(invitation != null)
                {
                    msg = "User is already invited in that team";
                }
                else
                {
                    context.TeamInvites.Add(new TeamInvite { InviterId = inviter.Id, InviteeId = invitee.Id, TeamId = team.Id, Inviter = inviter, Invitee = invitee, Team = team });
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                msg = "Something went wrong.";
                result = false;
            }

            return result;
        }

        public IEnumerable<TeamInvite> GetInvites(string userId)
        {
            var teamInvites = context.TeamInvites.Where(x => x.InviteeId == userId).ToList();
            return teamInvites;
        }

        public bool DeclineInvite(int inviteId)
        {
            var result = false;
            var invite = context.TeamInvites.Where(x => x.InviteId == inviteId).FirstOrDefault();
            if(invite != null)
            {
                try
                {
                    context.TeamInvites.Remove(invite);
                    context.SaveChanges();
                    result = true;
                }
                catch(Exception)
                {
                    result = false;
                }
            }
            return result;
        }
        public int GetRoleId(string roleName,int teamId)
        {
            TeamRole TeamRole = context.TeamRoles.FirstOrDefault(x => x.TeamId == teamId && x.Name==roleName);
            return TeamRole.Id;
            
        }
        public bool AcceptInvite(int inviteId)
        {
            var result = false;
            var invite= context.TeamInvites.Where(x => x.InviteId == inviteId).FirstOrDefault();
            
            if (invite != null)
            {
                var roleId = GetRoleId("Member", invite.TeamId);
                try
                {
                    AddMember(invite.InviteeId, invite.TeamId, roleId);
                    context.TeamInvites.Remove(invite);
                    context.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
        public int GetTeamIdFromInvite(int inviteId)
        {
            var invite = context.TeamInvites.Where(x => x.InviteId == inviteId).FirstOrDefault();
            return invite.TeamId;
        }
    }
}
