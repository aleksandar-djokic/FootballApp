using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TicketViewModel
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string UserSender { get; set; }
        public DateTime Time { get; set; }
        public bool isOpened { get; set; }
        public List<TicketMessageViewModel> messages { get; set; }
    }
}