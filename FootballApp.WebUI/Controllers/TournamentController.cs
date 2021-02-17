using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    [Authorize]
    public class TournamentController : Controller
    {
        private ITournamentRepository repo;
        public TournamentController(ITournamentRepository tournamentRepository)
        {
            this.repo = tournamentRepository;
        }
        // GET: Tournament
        public ActionResult Index()
        {
            var Tournaments = repo.ListActiveTournaments();
            var result = new List<TournamentViewModel>();
            foreach (var t in Tournaments)
            {
                var newVM = new TournamentViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    NumberOfParticipants = t.NumberOfParticipants,
                    NumberOfRounds = t.NumberOfRounds,
                    CurrentRound = t.CurrentRound,
                    isActive = t.isActive,
                    Winner = (t.Winner != null) ? t.Winner.Name : "",
                    RunnerUp = (t.RunnerUp != null) ? t.RunnerUp.Name : ""

                };
                newVM.CurrentNumberParticipants = repo.GetNumberOfParticipants(t.Id);

                result.Add(newVM);
            }
            return View(result);
        }
        [HttpGet]
        public ActionResult TournamentProfile(int id)
        {
            var profileVM = new TournamentProifleViewModel();
            var t = repo.GetTournamentById(id);
            profileVM.Tournamnet = new TournamentViewModel
            {
                Id = t.Id,
                Name = t.Name,
                isActive = t.isActive,
                Winner = t.Winner != null ? t.Winner.Name : "",
                RunnerUp = t.RunnerUp != null ? t.RunnerUp.Name : "",
                NumberOfParticipants = t.NumberOfParticipants,
                NumberOfRounds = t.NumberOfRounds,
                CurrentRound = t.CurrentRound
            };
            profileVM.Tournamnet.CurrentNumberParticipants = repo.GetNumberOfParticipants(id);
            profileVM.Rounds = new List<TournamentRound>();
            if (profileVM.Tournamnet.CurrentRound > 0)
            {
                for(int i = 0; i < profileVM.Tournamnet.CurrentRound; i++)
                {
                    var newRound = new TournamentRound { matches = new List<TournamentMatchViewModel>() };
                    var roundMatches = repo.GetMatchesForRound(id, i+1).ToList();
                    for(int j = 0; j < roundMatches.Count(); j++)
                    {
                        var temp = roundMatches[j];
                        TournamentMatchViewModel matchVM = new TournamentMatchViewModel
                        {
                            Id = temp.Id,
                            Round = temp.Round,
                            ScoreP1 = temp.ScoreP1,
                            ScoreP2 = temp.ScoreP2,
                            isConcluded = temp.isConcluded,
                            Participant1 = temp.Participant1.Name,
                            Participant2 = temp.Participant2.Name,
                            Winner = temp.Winner != null ? temp.Winner.Name : ""
                        };
                        newRound.matches.Add(matchVM);
                    }
                    profileVM.Rounds.Add(newRound);
                }
            }
            return View(profileVM);
        }
        [HttpGet]
        public ActionResult Create()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateTournamentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                repo.Create(vm.Name, vm.NumberOfParticipants);
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpGet]
        public JsonResult ListAvailableTournaments(int id)
        {
            var Tournaments = repo.ListAvailableTournaments(id);
            var result = new List<TournamentViewModel>();
            foreach( var t in Tournaments)
            {
                var newVM = new TournamentViewModel {
                    Id = t.Id,
                    Name = t.Name,
                    NumberOfParticipants = t.NumberOfParticipants,
                    NumberOfRounds = t.NumberOfRounds,
                    CurrentRound = t.CurrentRound,
                    isActive = t.isActive,
                    Winner = (t.Winner!=null)?t.Winner.Name:"",
                    RunnerUp = (t.RunnerUp != null) ? t.RunnerUp.Name:""
             
                };
                newVM.CurrentNumberParticipants = repo.GetNumberOfParticipants(t.Id);

                result.Add(newVM);
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListParticipatingTournaments(int id)
        {
            var Tournaments = repo.ListParticipatingTournaments(id);
            var result = new List<TournamentViewModel>();
            foreach (var t in Tournaments)
            {
                var newVM = new TournamentViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    NumberOfParticipants = t.NumberOfParticipants,
                    NumberOfRounds = t.NumberOfRounds,
                    CurrentRound = t.CurrentRound,
                    isActive = t.isActive,
                    Winner = (t.Winner != null) ? t.Winner.Name : "",
                    RunnerUp = (t.RunnerUp != null) ? t.RunnerUp.Name : ""

                };
                newVM.CurrentNumberParticipants = repo.GetNumberOfParticipants(t.Id);

                result.Add(newVM);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListPreviousTournaments(int id)
        {
            var Tournaments = repo.ListPreviousTournaments(id);
            var result = new List<TournamentViewModel>();
            foreach (var t in Tournaments)
            {
                var newVM = new TournamentViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    NumberOfParticipants = t.NumberOfParticipants,
                    NumberOfRounds = t.NumberOfRounds,
                    CurrentRound = t.CurrentRound,
                    isActive = t.isActive,
                    Winner = (t.Winner != null) ? t.Winner.Name : "",
                    RunnerUp = (t.RunnerUp != null) ? t.RunnerUp.Name : ""

                };
                newVM.CurrentNumberParticipants = repo.GetNumberOfParticipants(t.Id);

                result.Add(newVM);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult JoinTournament(int teamId, int tournamentId)
        {
            TournamentViewModel tournamentVm=null;
            var joinResult = repo.JoinTournament(teamId, tournamentId);
            if (repo.isTournamentFull(tournamentId))
            {
                repo.CreateInitialPairings(tournamentId);
            }
            if (joinResult)
            {
                var tournament = repo.GetTournamentById(tournamentId);
                tournamentVm = new TournamentViewModel
                {
                    Id = tournament.Id,
                    Name = tournament.Name,
                    NumberOfParticipants = tournament.NumberOfParticipants,
                    NumberOfRounds = tournament.NumberOfRounds,
                    CurrentRound = tournament.CurrentRound,
                    isActive = tournament.isActive,
                    Winner = (tournament.Winner != null) ? tournament.Winner.Name : "",
                    RunnerUp = (tournament.RunnerUp != null) ? tournament.RunnerUp.Name : ""
                };
                tournamentVm.CurrentNumberParticipants = repo.GetNumberOfParticipants(tournamentId);
            }
            return Json(new { result=joinResult,tournament=tournamentVm});
        }
        [HttpGet]
        public JsonResult GetMatchDetails(int id)
        {
            var match = repo.GetMatchById(id);
            var matchVM = new TournamentMatchScoreViewModel { Team1Id = match.Participant1Id, Team1Name = match.Participant1.Name, Team2Id = match.Participant2Id, Team2Name = match.Participant2.Name};
            return Json(matchVM,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ConcludeMatch(int matchId, int score1, int score2, int winnerId)
        {
            var result = repo.ConcludeMatch(matchId, score1, score2, winnerId);
            return Json(result);
        }
    }
}