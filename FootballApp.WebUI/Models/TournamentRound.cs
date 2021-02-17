using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TournamentRound
    {
        public List<TournamentMatchViewModel> matches { get; set; }
    }
}