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
    }
}
