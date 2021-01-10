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
    public class MatchController : Controller
    {
        public IMatchRepository matches;

        public MatchController(IMatchRepository matchRepository)
        {
            this.matches = matchRepository;

        }

        [HttpGet]
        public JsonResult GetMatches(int teamId)
        {
            var listOfMatches= matches.getActiveMatches(teamId, User.Identity.GetUserId()).ToList();
            var result = new List<MatchViewModel>();
            for (int i = 0; i < listOfMatches.Count; i++)
            {
                var current = listOfMatches[i];
                var temp = new MatchViewModel() { Id = current.Id, Team1Name = current.Team1.Name, Team2Name = current.Team2.Name, Location = current.Adress, Time = current.DateTime.ToString() };
                if (current.Team1.Picture != null)
                {
                    string imageBase64 = Convert.ToBase64String(current.Team1.Picture);
                    temp.Team1Image = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                else
                {
                    temp.Team1Image = "";
                }
                if (current.Team2.Picture != null)
                {
                    string imageBase64 = Convert.ToBase64String(current.Team2.Picture);
                    temp.Team2Image = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                else
                {
                    temp.Team2Image = "";
                }
                result.Add(temp);
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(int team1Id, string team2Name, string Adress, DateTime dateTime)
        {
            var resultmsg = "";
            var resultvalue = false;
            if (matches.Create(team1Id, team2Name, dateTime, Adress, out resultmsg,User.Identity.GetUserId()))
            {
                resultvalue = true;
            }
            var result = new { resultvalue = resultvalue, resultmsg = resultmsg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetPendingMatches(int teamid)
        {
            var listOfMatches = matches.getPendingMatches(teamid,User.Identity.GetUserId()).ToList();
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
        [HttpPost]
        public JsonResult Accept(int matchId)
        {
            var resultvalue = matches.Accept(matchId, User.Identity.GetUserId());
            var resultmsg = resultvalue ? "success" : "Match date already passed.";
            var result = new { resultvalue = resultvalue, resultmsg = resultmsg };

            return Json(result);
        }
        [HttpPost]
        public JsonResult Decline(int matchId)
        {
            var result = matches.Decline(matchId);
            return Json(result);
        }
    }
}