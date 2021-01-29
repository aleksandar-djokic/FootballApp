using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TicketMessageViewModel
    {
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}