using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface IMatchRepository
    {
        bool Create(int team1Id, string team2Name, DateTime dateTime, string Adress, out string resultmsg,string userId);
        IEnumerable<Match> getPendingMatches(int teamId,string userId);
        IEnumerable<Match> getActiveMatches(int teamId,string userId);
        bool Accept(int matchId, string userId);
        bool Decline(int matchId);
    }
}
