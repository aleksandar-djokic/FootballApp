using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class PendingMatchViewModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public string Location { get; set; }
        public string ImageSource { get; set; }
        public string Date { get; set; }
    }
}