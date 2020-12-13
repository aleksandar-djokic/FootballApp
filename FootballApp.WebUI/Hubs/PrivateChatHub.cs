using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Hubs
{
    public class PrivateChatHub : Hub
    {
        public void JoinGroup(string grp)
        {
            this.Groups.Add(this.Context.ConnectionId, grp);
        }
        public static void Send(string sender,string reciever, string message, string DateTime, int conversation)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PrivateChatHub>();
            context.Clients.Group(reciever).addNewMessageToPage(sender, message, DateTime,conversation);
        }
    }
}