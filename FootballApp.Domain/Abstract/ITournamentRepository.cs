using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface ITournamentRepository
    {
        void Create(string Name, int NumberOfParticipants);
        int NumberOfRounds(int NumberOfParticipants);
        IEnumerable<Tournament> ListAvailableTournaments(int teamId);
        int GetNumberOfParticipants(int id);
        IEnumerable<Tournament> ListParticipatingTournaments(int teamId);
        IEnumerable<Tournament> ListPreviousTournaments(int teamId);
        IEnumerable<Tournament> ListActiveTournaments();
        bool JoinTournament(int teamId, int tournamentId);
        Tournament GetTournamentById(int id);
        bool isTournamentFull(int tournamentId);
        void CreateInitialPairings(int tournamentId);
        IEnumerable<TournamentMatch> GetMatchesForRound(int id,int roundNum);
        TournamentMatch GetMatchById(int id);
        bool ConcludeMatch(int matchId, int score1, int score2, int winnerId);
        void ConcludeTournament(int tournamentId);
        void PairNextRound(int tournamentId);
    }
}
