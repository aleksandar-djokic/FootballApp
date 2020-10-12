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
    }
}