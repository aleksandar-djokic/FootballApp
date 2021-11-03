using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class TournamentRepository:ITournamentRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public bool ConcludeMatch(int matchId, int score1, int score2, int winnerId)
        {
            var result = false;
            //conclude match
            var match = context.TournamentMatches.First(x => x.Id == matchId);
            try
            {
                match.ScoreP1 = score1;
                match.ScoreP2 = score2;
                match.WinnerId = winnerId;
                match.isConcluded = true;
                context.SaveChanges();
                result = true;

            }
            catch
            {
                result = false;
            }
            //check if last round
            var tournament = context.Tournaments.First(x => x.Id == match.TournamentId);
            //if it is conclude tournament
            if (tournament.CurrentRound == tournament.NumberOfRounds)
            {
                ConcludeTournament(tournament.Id);
            }
            else//check if all matches are concluded???
            {
                var allMatchesNotFinished = (from m in context.TournamentMatches
                                          where m.TournamentId == tournament.Id && m.Round == tournament.CurrentRound
                                          select m.isConcluded).Contains(false);
                if (!allMatchesNotFinished)
                {

                    PairNextRound(tournament.Id);
                }
            }
            //else pair next round
            return result;
        }

        public void ConcludeTournament(int tournamentId)
        {
            var tournament = context.Tournaments.First(x => x.Id == tournamentId);
            var lastMatch = context.TournamentMatches.First(x => x.TournamentId == tournament.Id && x.Round == tournament.CurrentRound);
            tournament.WinnerId = lastMatch.WinnerId;
            tournament.RunnerUpId= lastMatch.WinnerId == lastMatch.Participant1Id ? lastMatch.Participant2Id : lastMatch.Participant1Id;
            tournament.isActive = false;
            context.SaveChanges();

        }

        public void Create(string Name, int NumberOfParticipants)
        {
            var tournamet = new Tournament { Name = Name, NumberOfParticipants = NumberOfParticipants, CurrentRound = 0,isActive=true };
            tournamet.NumberOfRounds = NumberOfRounds(NumberOfParticipants);
            try
            {
                context.Tournaments.Add(tournamet);
                
            }
            catch
            {
                
            }
            finally
            {
                context.SaveChanges();
            }
            
        }

        public void CreateInitialPairings(int tournamentId)
        {
            //select all teams that are participating
            var tournamentParticipants = (from t in context.TournamentParticipants
                                          where t.TournamentId == tournamentId
                                          select t).ToList();
            // go throught them and pair them together in a match
            for(int i = 1; i <= (tournamentParticipants.Count() - 1); i += 2)
            {
                
                context.TournamentMatches.Add(new TournamentMatch { Participant1Id = tournamentParticipants[i - 1].TeamId, Participant2Id = tournamentParticipants[i].TeamId, Round = 1, TournamentId = tournamentId });
                context.SaveChanges();
            }
            
            var tournament = context.Tournaments.First(x => x.Id == tournamentId);
            tournament.CurrentRound = 1;
            context.SaveChanges();
            
        }

        public TournamentMatch GetMatchById(int id)
        {
            return context.TournamentMatches.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TournamentMatch> GetMatchesForRound(int id, int roundNum)
        {
            var matches = (from m in context.TournamentMatches
                           where m.TournamentId == id && m.Round == roundNum
                           select m).ToList();
            return matches;
        }

        public int GetNumberOfParticipants(int id)
        {
            return context.TournamentParticipants.Where(x => x.TournamentId == id).Count();
        }

        public Tournament GetTournamentById(int id)
        {
            return context.Tournaments.FirstOrDefault(x => x.Id == id);
        }

        public bool isTournamentFull(int tournamentId)
        {
            var result = false;
            var participantsMax = (from t in context.Tournaments
                                where t.Id == tournamentId
                                select  t.NumberOfParticipants).First();
            var participants = (from p in context.TournamentParticipants
                                where p.TournamentId == tournamentId
                                select p).ToList().Count();
            if (participants == participantsMax)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool JoinTournament(int teamId, int tournamentId)
        {
            var result = false;
            try
            {
                context.TournamentParticipants.Add(new TournamentParticipants { TeamId = teamId, TournamentId = tournamentId });
                context.SaveChanges();
                
                result = true;
            }
            catch
            {

                result = false;
            }
            return result;

        }

        public IEnumerable<Tournament> ListActiveTournaments()
        {
            return context.Tournaments.Where(x => x.isActive == true).ToList();
        }

        public IEnumerable<Tournament> ListAvailableTournaments(int teamId)
        {
            var tournaments = (from t in context.Tournaments
                               where t.NumberOfParticipants > (from p in context.TournamentParticipants where p.TournamentId == t.Id select p).Count() && !(from p in context.TournamentParticipants where p.TournamentId == t.Id select p.TeamId).Contains(teamId) && t.isActive == true
                               select new
                               {
                                   Id = t.Id,
                                   Name = t.Name,
                                   NumberOfParticipants = t.NumberOfParticipants,
                                   NumberOfRounds = t.NumberOfRounds,
                                   CurrentRound = t.CurrentRound,
                                   isActive = t.isActive,
                                   WinnerId = t.WinnerId,
                                   RunnerUpId = t.RunnerUpId,
                                   Winner = t.Winner,
                                   RunnerUp = t.RunnerUp
                               })
                              .AsEnumerable().Select(t => new Tournament
                              {
                                  Id = t.Id,
                                  Name = t.Name,
                                  NumberOfParticipants = t.NumberOfParticipants,
                                  NumberOfRounds = t.NumberOfRounds,
                                  CurrentRound = t.CurrentRound,
                                  isActive = t.isActive,
                                  WinnerId = t.WinnerId,
                                  RunnerUpId = t.RunnerUpId,
                                  Winner = t.Winner,
                                  RunnerUp = t.RunnerUp
                              }).ToList();

            return tournaments;
        }

        public IEnumerable<Tournament> ListParticipatingTournaments(int teamId)
        {
            var tournaments = (from t in context.Tournaments
                              where t.isActive == true && (from p in context.TournamentParticipants
                                                           where p.TournamentId == t.Id
                                                           select p.TeamId).Contains(teamId)
                              select new
                              {
                                  Id = t.Id,
                                  Name = t.Name,
                                  NumberOfParticipants = t.NumberOfParticipants,
                                  NumberOfRounds = t.NumberOfRounds,
                                  CurrentRound = t.CurrentRound,
                                  isActive = t.isActive,
                                  WinnerId = t.WinnerId,
                                  RunnerUpId = t.RunnerUpId,
                                  Winner = t.Winner,
                                  RunnerUp = t.RunnerUp
                              }).AsEnumerable().Select(t => new Tournament
                              {
                                  Id = t.Id,
                                  Name = t.Name,
                                  NumberOfParticipants = t.NumberOfParticipants,
                                  NumberOfRounds = t.NumberOfRounds,
                                  CurrentRound = t.CurrentRound,
                                  isActive = t.isActive,
                                  WinnerId = t.WinnerId,
                                  RunnerUpId = t.RunnerUpId,
                                  Winner = t.Winner,
                                  RunnerUp = t.RunnerUp
                              }).ToList();
            return tournaments;



        }

        public IEnumerable<Tournament> ListPreviousTournaments(int teamId)
        {
            var tournaments = (from t in context.Tournaments
                               where t.isActive == false && (from p in context.TournamentParticipants
                                                            where p.TournamentId == t.Id
                                                            select p.TeamId).Contains(teamId)
                               select new
                               {
                                   Id = t.Id,
                                   Name = t.Name,
                                   NumberOfParticipants = t.NumberOfParticipants,
                                   NumberOfRounds = t.NumberOfRounds,
                                   CurrentRound = t.CurrentRound,
                                   isActive = t.isActive,
                                   WinnerId = t.WinnerId,
                                   RunnerUpId = t.RunnerUpId,
                                   Winner = t.Winner,
                                   RunnerUp = t.RunnerUp
                               }).AsEnumerable().Select(t => new Tournament
                               {
                                   Id = t.Id,
                                   Name = t.Name,
                                   NumberOfParticipants = t.NumberOfParticipants,
                                   NumberOfRounds = t.NumberOfRounds,
                                   CurrentRound = t.CurrentRound,
                                   isActive = t.isActive,
                                   WinnerId = t.WinnerId,
                                   RunnerUpId = t.RunnerUpId,
                                   Winner = t.Winner,
                                   RunnerUp = t.RunnerUp
                               }).ToList();
            return tournaments;
        }

        public int NumberOfRounds(int NumberOfParticipants)
        {
            int i = 0;
            while (NumberOfParticipants > 1)
            {
                i++;
                NumberOfParticipants = NumberOfParticipants / 2;
            }
            return i;
        }

        public void PairNextRound(int tournamentId)
        {
            var tournament = context.Tournaments.First(x => x.Id == tournamentId);
            var winners = (from m in context.TournamentMatches
                                          where m.TournamentId == tournamentId && m.Round==tournament.CurrentRound
                                          select m.Winner).ToList();
            // go throught them and pair them together in a match
            for (int i = 1; i <= (winners.Count() - 1); i += 2)
            {

                context.TournamentMatches.Add(new TournamentMatch { Participant1Id = winners[i - 1].Id, Participant2Id = winners[i].Id, Round = (tournament.CurrentRound + 1).GetValueOrDefault(), TournamentId = tournamentId });
                context.SaveChanges();
            }
            tournament.CurrentRound += 1;
            context.SaveChanges();
        }
    }
}
