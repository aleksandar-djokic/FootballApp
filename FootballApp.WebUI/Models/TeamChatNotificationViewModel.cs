using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TeamChatNotificationViewModel
    {
        public int NotificationId { get; set;}
        public int TeamId { get; set; }
        public int MessageId { get; set; }
    }
}