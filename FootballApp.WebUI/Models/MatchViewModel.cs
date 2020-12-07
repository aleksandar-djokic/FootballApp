using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string Team1Name { get; set; }
        public string Team1Image { get; set; }
        public string Team2Name { get; set; }
        public string Team2Image{get;set;}
        public string Time { get; set; }
        public string Location { get; set; }

    }
}