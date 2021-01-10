using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TeamMatchNotificationViewModel
    {
        public int NotificationId { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public string Type { get; set; }
    }
}