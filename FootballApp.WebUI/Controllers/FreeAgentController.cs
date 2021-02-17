using FootballApp.Domain.Abstract;
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
    public class FreeAgentController : Controller
    {
        public IFreeAgentRepository agents;
        public FreeAgentController(IFreeAgentRepository agents)
        {
            this.agents = agents;
        }
        // GET: FreeAgent
        [HttpGet]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var profile = agents.getFreeAgent(id);
            if (profile != null)
            {
                var vm = new FreeAgentViewModel { Id = profile.Id, UserId = profile.UserId, Country = profile.Country, City = profile.City, Active = profile.Active };
                return View("Edit",vm);
            }
            else
            {
                return View("Create");
            }

        }
        [HttpPost]
        public JsonResult Create(string countryValue, string cityValue)
        {
            var id = User.Identity.GetUserId();
            var result = agents.CreateProfile(id,countryValue,cityValue);
            return Json(result);
        }
        [HttpPost]
        public ActionResult Edit(FreeAgentViewModel agentViewModel)
        {
            agents.Edit(agentViewModel.Id, agentViewModel.Country, agentViewModel.City, agentViewModel.Active);
            return View("Edit", agentViewModel);
        }
        [HttpGet]
        public JsonResult GetFreeAgents(int teamId)
        {
            var freeAgents = agents.getFreeAgents(teamId);
            List<FreeAgentProfileViewModel> result = new List<FreeAgentProfileViewModel>();
            foreach(var f in freeAgents)
            {
                var imagesource = "";
                var user = agents.getUser(f.UserId);
                if (user.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                result.Add(new FreeAgentProfileViewModel { FreeAgentId = f.Id, UserId = user.Id, Name = user.UserName, ImageSource = imagesource, Country=f.Country, City=f.City });
            }
            result.OrderBy(x => x.Country).ThenBy(x => x.City);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SendRequestToFreeAgent(int agentId,int teamId)
        {
            var hasRequested =agents.isRequested(agentId,teamId);
            var result = false;
            if (hasRequested)
            {
                result=agents.AddAgentToTeam(agentId, teamId);
                
                
            }
            else
            {
                result=agents.SendRequestToAgent(agentId, teamId);
                
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SearchAgents(int teamId,string Country,string City)
        {
            var freeAgents = agents.SearchFreeAgents(teamId, Country, City);
            List<FreeAgentProfileViewModel> result = new List<FreeAgentProfileViewModel>();
            foreach (var f in freeAgents)
            {
                var imagesource = "";
                var user = agents.getUser(f.UserId);
                if (user.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                result.Add(new FreeAgentProfileViewModel { FreeAgentId = f.Id, UserId = user.Id, Name = user.UserName, ImageSource = imagesource, Country = f.Country, City = f.City });
            }
            result.OrderBy(x => x.Country).ThenBy(x => x.City);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}