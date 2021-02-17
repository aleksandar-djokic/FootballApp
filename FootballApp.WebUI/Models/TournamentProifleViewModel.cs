using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TournamentProifleViewModel
    {
        public TournamentViewModel Tournamnet { get; set; }
        public List<TournamentRound> Rounds { get; set; }
    }
}