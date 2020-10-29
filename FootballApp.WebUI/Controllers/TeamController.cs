using FootballApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballApp.Domain.Abstract;
using System.IO;
using Microsoft.AspNet.Identity;

namespace FootballApp.WebUI.Controllers
{
    public class TeamController : Controller
    {
        public ITeamRepository teams;
     
        public TeamController(ITeamRepository teamRepository)
        {
            this.teams = teamRepository;
           
        }
        // GET: Team
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var membersTeams = teams.GetTeamsByMember(userid);
            List<TeamsDisplayViewModel> result = new List<TeamsDisplayViewModel>();
            foreach(var t in membersTeams)
            {
                string imagesource = "";
                if (t.Picture != null)
                {
                    string imageBase64 = Convert.ToBase64String(t.Picture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                   
                }
                result.Add(new TeamsDisplayViewModel { Id = t.Id, Name = t.Name, Description = t.Description, ImageSource = imagesource });
            }
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamViewModel team)
        {
            byte[] imageData = null;
            if (ModelState.IsValid)
            {
                if (team.Picture != null)
                {


                    using (BinaryReader br = new BinaryReader(team.Picture.InputStream))
                    {
                        team.Picture.InputStream.Position = 0;
                        imageData = br.ReadBytes(team.Picture.ContentLength);
                    }



                }
                var userid = User.Identity.GetUserId();
                teams.Create(team.TeamName, team.Description, imageData, userid);
                return View();
            }
          
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int TeamId)
        {
            var Team = teams.Teams.First(x => x.Id == TeamId);
            TeamViewModel teamForEdit = new TeamViewModel()
            {
                Id=Team.Id,   
                TeamName = Team.Name,
                Description = Team.Description
            };
            return View(teamForEdit);
        }
        [HttpPost]
        public ActionResult Edit(TeamViewModel team)
        {
            byte[] imageData = null;
            if (ModelState.IsValid)
            {
                if (team.Picture != null)
                {


                    using (BinaryReader br = new BinaryReader(team.Picture.InputStream))
                    {
                        team.Picture.InputStream.Position = 0;
                        imageData = br.ReadBytes(team.Picture.ContentLength);
                    }



                }
                teams.Edit(team.Id, team.TeamName, team.Description, imageData);
                return View();

            }
            return View();
        }
        public JsonResult SearchTeam(string Name)
        {
            var teamsearch = teams.SearchTeam(Name);
            List<TeamsDisplayViewModel> teamlist = new List<TeamsDisplayViewModel>();
            foreach (var t in teamsearch)
            {
                string imagesource = "";
                if (t.Picture != null)
                {
                    string imageBase64 = Convert.ToBase64String(t.Picture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                }
                teamlist.Add(new TeamsDisplayViewModel { Id = t.Id, Name = t.Name, Description = t.Description, ImageSource = imagesource });
            }
            var result = new { teamlist = teamlist };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TeamProfile(int teamId)
        {
            var team = teams.GetTeamByID(teamId);
            string imagesource = "";
            if (team.Picture != null)
            {
                string imageBase64 = Convert.ToBase64String(team.Picture);
                imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
            }

            TeamsDisplayViewModel teamDisplay = new TeamsDisplayViewModel { Id = team.Id,Name=team.Name,Description=team.Description,ImageSource=imagesource };
            return View(teamDisplay);
        }
        [HttpPost]
        public JsonResult Invite(string inviteeId,int teamId)
        {
            var userid = User.Identity.GetUserId();
            var msg = "";
            var value = false;
            if (teams.InviteUser(userid, inviteeId, teamId,out msg))
            {
                value = true;
            }
            var result = new { value = value, msg = msg };
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetTeams()
        {
            var userid = User.Identity.GetUserId();
            var membersTeams = teams.GetTeamsByMember(userid);
            List<TeamsDisplayViewModel> result = new List<TeamsDisplayViewModel>();
            foreach (var t in membersTeams)
            {
                string imagesource = "";
                if (t.Picture != null)
                {
                    string imageBase64 = Convert.ToBase64String(t.Picture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                }
                result.Add(new TeamsDisplayViewModel { Id = t.Id, Name = t.Name, Description = t.Description, ImageSource = imagesource });
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTeamInvites()
        {
            var userid = User.Identity.GetUserId();
            var invites = teams.GetInvites(userid);
            List<TeamInviteViewModel> result = new List<TeamInviteViewModel>();
            foreach(var i in invites)
            {
                string imagesource = "";
                if (i.Team.Picture != null)
                {
                    string imageBase64 = Convert.ToBase64String(i.Team.Picture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                result.Add(new TeamInviteViewModel { InviteId = i.InviteId, ImageSource = imagesource, TeamName = i.Team.Name, FriendName = i.Inviter.UserName });
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeclineTeamInvite(int inviteId)
        {
            var result = teams.DeclineInvite(inviteId);
            return Json(result);
        }
        [HttpPost]
        public JsonResult AcceptTeamInvite(int inviteId)
        {
            var teamId = teams.GetTeamIdFromInvite(inviteId);
            var team = teams.GetTeamByID(teamId);
            var value = teams.AcceptInvite(inviteId);
            string imagesource = "";
            if (team.Picture != null)
            {
                string imageBase64 = Convert.ToBase64String(team.Picture);
                imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
            }
            var result = new { value = value, team = new TeamsDisplayViewModel { Id = team.Id, Name = team.Name, Description = team.Description, ImageSource = imagesource } };
            return Json(result);
        }
    }
}