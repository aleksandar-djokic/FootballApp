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

        public bool Accept(int matchId,string userId)
        {
            var match = context.Matches.FirstOrDefault(x => x.Id == matchId);
            var result = false;
            var adminsTeam2 = context.TeamMembers.Where(x => x.TeamId == match.Team2Id && (x.TeamRole.Name == "Admin" || x.TeamRole.Name == "Owner")).ToList();
            foreach(var a in adminsTeam2)
            {
                var notification = context.Notifications.OfType<TeamMatchNotification>().Where(x => x.MatchId == match.Id && x.RecieverId == a.UserId).FirstOrDefault();
                if (notification != null)
                {
                    context.Notifications.Remove(notification);

                }
            }
            if (match.DateTime > DateTime.Now)
            {
                match.isAccepted = true;
                var peopleToNotify = context.TeamMembers.Where(x => x.TeamId == match.Team1Id || x.TeamId == match.Team2Id).ToList();
                foreach(var p in peopleToNotify)
                {
                    if (userId != p.UserId)
                    {

                        var newNotification = new TeamMatchNotification { isRead = false, RecieverId = p.UserId, MatchId = match.Id, TeamId = p.TeamId, Type = "New" };
                        context.Notifications.Add(newNotification);
                    }

                }
                context.SaveChanges();
                
                result = true;
            }
            else
            {
                context.Matches.Remove(match);
                context.SaveChanges();
                result = false;
            }
            return result;
        }

        public bool Create(int team1Id, string team2Name, DateTime dateTime, string Adress, out string resultmsg, string userId)
        {
            resultmsg = "";
            var result = false;
            var team1 = context.Teams.FirstOrDefault(x => x.Id == team1Id);
            var team2 = context.Teams.FirstOrDefault(x => x.Name.ToLower().Equals(team2Name.ToLower()));
            if (team2 != null)
            {
                if(team1.Id!=team2.Id){
                    try
                    {
                        var newMatch = new Match { Team1Id = team1Id, Team2Id = team2.Id, Adress = Adress, DateTime = dateTime };
                        context.Matches.Add(newMatch);
                        var admins = context.TeamMembers.Where(x => x.TeamId == team2.Id && (x.TeamRole.Name == "Admin" || x.TeamRole.Name == "Owner")).ToList();
                        foreach(var a in admins)
                        {
                            if (userId != a.UserId)
                            {
                                var newNotification = new TeamMatchNotification { isRead = false, RecieverId = a.UserId, MatchId = newMatch.Id, TeamId = a.TeamId,Type="Pending" };
                                context.Notifications.Add(newNotification);

                            }
                        }
                        resultmsg = "Uspešno napravljen meč.";
                        result = true;
                    }
                    catch
                    {
                        resultmsg = "Došlo je do greške, nije moguće napraviti meč.";
                        result = false;
                    }
                    context.SaveChanges();
                }
                else{
                    resultmsg = "Tim ne može izazvati sam sebe na meč.";
                    result = false;
                }
                
            }
            else
            {
                resultmsg = "Nema tima sa unesenim imenom.";
                result = false;
            }
            return result;
        }

        public bool Decline(int matchId)
        {
            var match = context.Matches.FirstOrDefault(x => x.Id == matchId);
            var result = false;
            try
            {
                var notifications = context.Notifications.OfType<TeamMatchNotification>().Where(x => x.MatchId == matchId).ToList();
                foreach(var n in notifications)
                {
                    context.Notifications.Remove(n);
                }
                context.Matches.Remove(match);
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public IEnumerable<Match> getActiveMatches(int teamId,string userId)
        {
            var notifications = context.Notifications.OfType<TeamMatchNotification>().Where(x => x.TeamId == teamId && x.RecieverId == userId && x.isRead == false && x.Type == "New").ToList();
            if (notifications.Count() > 0)
            {
                foreach(var n in notifications)
                {
                    n.isRead = true;
                    context.SaveChanges();
                }
            }
            return context.Matches.Where(x => (x.Team1Id == teamId || x.Team2Id == teamId) && x.isAccepted == true && x.DateTime > DateTime.Now).ToList();

        }

        public IEnumerable<Match> getPendingMatches(int teamId,string userId)
        {
            var notifications = context.Notifications.OfType<TeamMatchNotification>().Where(x => x.TeamId == teamId && x.RecieverId == userId && x.isRead == false && x.Type == "Pending").ToList();
            if (notifications.Count() > 0)
            {
                foreach (var n in notifications)
                {
                    n.isRead = true;
                    context.SaveChanges();
                }
            }
            return context.Matches.Where(x => x.isAccepted == false && (x.Team1Id == teamId || x.Team2Id == teamId));
        }
    }
}
