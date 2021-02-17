using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TournamentMatchViewModel
    {
        public int Id { get; set; }
        public int Round { get; set; }
        public int? ScoreP1 { get; set; }
        public int? ScoreP2 { get; set; }
        public bool isConcluded { get; set; } = false;
        public string Participant1 { get; set; }
        public string Participant2 { get; set; }
        public string Winner { get; set; } = "";
    }
}