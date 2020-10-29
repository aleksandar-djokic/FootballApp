using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TeamInviteViewModel
    {
        public int InviteId { get; set; }
        public string ImageSource { get; set; }
        public string TeamName { get; set; }
        public string FriendName { get; set; }

    }
}