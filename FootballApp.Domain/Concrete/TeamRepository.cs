﻿using FootballApp.Domain.Abstract;
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

        public void AddMember(string UserId, int TeamId)
        {
            ApplicationUser User = context.Users.First(x => x.Id == UserId);
            Team Team = context.Teams.First(x => x.Id == TeamId);
            TeamMembers newMember = new TeamMembers
            {
                TeamId = TeamId,
                UserId = UserId,
                Team=Team,
                User=User
            };
            context.TeamMembers.Add(newMember);
            context.SaveChanges();
        }

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
            AddMember(user, team.Id);
        }
    }
}
