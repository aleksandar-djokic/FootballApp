using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Hubs;
using FootballApp.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    [Authorize(Roles ="User")]
    public class TeamChatController : Controller
    {
        public ITeamChatRepository teamchat;
        public TeamChatController(ITeamChatRepository teamchatRepository)
        {
            this.teamchat = teamchatRepository;

        }
        // GET: TeamChat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendMsg(string message, string grp,int teamId)
        {
            var userId= User.Identity.GetUserId();
            var user = teamchat.GetUser(userId);
            var result = teamchat.AddMessage(userId,teamId,message, DateTime.Now);
            if (result)
            {

                string imagesource = "";
                if (user.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                }
                TeamChatHub.Send(user.UserName, message,imagesource,DateTime.Now.ToString(),grp);
                var members = teamchat.GetTeamMembers(teamId);
                foreach(var m in members)
                {
                    if( m.Id != userId)
                    {

                        NotificationHub.SendTeamNotification(teamId, m.UserName);
                    }
                }
            }
           
            return Json(result);
        }
        
        public JsonResult GetMessages(int? messageCount,int teamid)
        {
            var messages = teamchat.GetMessages(messageCount,teamid).ToList();
            var result = new List<TeamChatMessageViewModel>();
            for(int i=0;i<messages.Count();i++)
            {
                var current = messages[i];
                string imagesource = "";
                if (current.User.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(current.User.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                result.Add(new TeamChatMessageViewModel { UserName = current.User.UserName, Time = current.Time.ToString(), Message = current.Message, 
                    ImageSource = imagesource });
            }
            teamchat.readNotifications(User.Identity.GetUserId(), teamid);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public void ReadNotifications(int teamId)
        {
            teamchat.readNotifications(User.Identity.GetUserId(), teamId);
        }

    }
}