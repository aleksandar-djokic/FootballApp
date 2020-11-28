using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    public class MatchController : Controller
    {
        public IMatchRepository matches;

        public MatchController(IMatchRepository matchRepository)
        {
            this.matches = matchRepository;

        }
        [HttpPost]
        public JsonResult Create(int team1Id, string team2Name, string Adress, DateTime dateTime)
        {
            var resultmsg = "";
            var resultvalue = false;
            if (matches.Create(team1Id, team2Name, dateTime, Adress, out resultmsg))
            {
                resultvalue = true;
            }
            var result = new { resultvalue = resultvalue, resultmsg = resultmsg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetPendingMatches(int teamid)
        {
            var listOfMatches = matches.getPendingMatches(teamid).ToList();
            var result = new List<PendingMatchViewModel>();
            
            for(int i = 0; i < listOfMatches.Count(); i++)
            {
                var current = listOfMatches[i];
                var temp = new PendingMatchViewModel() { Id = current.Id, Date = current.DateTime.ToString(), Location = current.Adress, ImageSource = "",TeamId=current.Team1Id};
                if (teamid == current.Team1Id)
                {
                    temp.TeamName = current.Team2.Name;
                    if (current.Team2.Picture != null)
                    {
                        string imageBase64 = Convert.ToBase64String(current.Team2.Picture);
                        temp.ImageSource = string.Format("data:image/png;base64,{0}", imageBase64);
                    }

                }
                else
                {
                    temp.TeamName = current.Team1.Name;
                    if (current.Team1.Picture != null)
                    {
                        string imageBase64 = Convert.ToBase64String(current.Team1.Picture);
                        temp.ImageSource = string.Format("data:image/png;base64,{0}", imageBase64);
                    }

                }
                result.Add(temp);
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}