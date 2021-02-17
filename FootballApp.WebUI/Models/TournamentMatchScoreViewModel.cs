using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TournamentMatchScoreViewModel
    {
        public int Team1Id { get; set; }
        public string Team1Name { get; set; }
        public int Team2Id { get; set; }
        public string Team2Name { get; set; }
        
    }
}