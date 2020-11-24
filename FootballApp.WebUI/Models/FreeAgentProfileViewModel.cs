using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class FreeAgentProfileViewModel
    {
        public int FreeAgentId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}