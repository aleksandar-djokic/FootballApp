using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string myName { get; set; }
        public bool areFriends { get; set; }
        public string UserName { get; set; }
        public string myImageSource { get; set; }
        public string ImageSource { get; set; }
    }
}