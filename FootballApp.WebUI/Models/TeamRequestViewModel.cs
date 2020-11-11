using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TeamRequestViewModel
    {
        public int RequestId { get; set; }
        public string ImageSource { get; set; }
        public string TeamName { get; set; }
        public string Username { get; set; }
        public string Requestor { get; set; }
    }
}