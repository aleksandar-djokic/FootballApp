using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class FriendRequestViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        //Just to know if its Incoming or Outgoing request for our user
        public string Direction { get; set; }
    }
}