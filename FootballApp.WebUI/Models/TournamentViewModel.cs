using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? NumberOfParticipants { get; set; }
        public int? NumberOfRounds { get; set; }
        public int? CurrentRound { get; set; }
        public bool isActive { get; set; }
        public string Winner { get; set; } = "";
        public string RunnerUp { get; set; } = "";
        public int CurrentNumberParticipants { get; set; } = 0;
    }
}