using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Hubs
{
    public class TeamChatHub : Hub
    {
        public void JoinGroup(string grp)
        {
            this.Groups.Add(this.Context.ConnectionId, grp);
        }
        public static void Send(string name,string message,string imgsource,string DateTime,string grp)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TeamChatHub>();
            context.Clients.Group(grp).addNewMessageToPage(name, message,imgsource,DateTime);
        }
    }
}