using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Hubs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
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
        public JsonResult SendMsg(string name, string message, string grp)
        {
            var userId= User.Identity.GetUserId();
            var user = teamchat.GetUser(userId);
            string imagesource = "";
            if (user.ProfilePicture != null)
            {
                string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

            }
            TeamChatHub.Send(user.UserName, message,imagesource,DateTime.Now.ToString(),grp);
            return Json("true");
        }

    }
}