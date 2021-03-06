using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class ConversationViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ImageSource { get; set; }
        public string MessageSender { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public bool isRead { get; set; } = false;
    }
}