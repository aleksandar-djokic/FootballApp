using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class MatchRepository:IMatchRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public bool Create(int team1Id, string team2Name, DateTime dateTime, string Adress, out string resultmsg)
        {
            resultmsg = "";
            var result = false;
            var team1 = context.Teams.FirstOrDefault(x => x.Id == team1Id);
            var team2 = context.Teams.FirstOrDefault(x => x.Name.ToLower().Equals(team2Name.ToLower()));
            if (team2 != null)
            {
                try
                {
                    context.Matches.Add(new Match { Team1Id = team1Id, Team2Id = team2.Id, Adress = Adress, DateTime = dateTime });
                    resultmsg = "Successfully created a match.";
                    result = true;
                }
                catch
                {
                    resultmsg = "An error occured,unable to create match.";
                    result = false;
                }
                context.SaveChanges();
                
            }
            else
            {
                resultmsg = "No team with that name found.";
                result = false;
            }
            return result;
        }

        public IEnumerable<Match> getPendingMatches(int teamId)
        {
            return context.Matches.Where(x => x.isAccepted == false && (x.Team1Id == teamId || x.Team2Id == teamId));
        }
    }
}
