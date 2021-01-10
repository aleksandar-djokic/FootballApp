using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Hubs
{
    public class NotificationHub : Hub
    {
       
        public void JoinGroup(string grp)
        {
            this.Groups.Add(this.Context.ConnectionId, grp);
        }
        public static void SendTeamNotification(int teamId, string grp)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.Group(grp).addNewTeamNotific(teamId);
        }
        public static void SendChatNotification(int conversationId, string grp)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.Group(grp).addNewChatNotific(conversationId);
        }
    }
}