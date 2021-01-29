using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TicketAdminViewModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}