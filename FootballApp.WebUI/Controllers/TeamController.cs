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
            return View();
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
            TeamEditViewModel teamForEdit = new TeamEditViewModel()
            {
                Id=Team.Id,   
                TeamName = Team.Name,
                Description = Team.Description
            };
            return View(teamForEdit);
        }
        [HttpPost]
        public ActionResult Edit(TeamEditViewModel team)
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
    }
}