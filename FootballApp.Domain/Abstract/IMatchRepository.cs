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
        bool Create(int team1Id, string team2Name, DateTime dateTime, string Adress, out string resultmsg);
        IEnumerable<Match> getPendingMatches(int teamId);
        IEnumerable<Match> getActiveMatches(int teamId);
        bool Accept(int matchId);
        bool Decline(int matchId);
    }
}
