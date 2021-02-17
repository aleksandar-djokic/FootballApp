using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public INewsRepository newsRepo;
        public HomeController(INewsRepository news)
        {
            this.newsRepo = news;
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Landing");
            }
            var news  = newsRepo.GetLastFive().ToList();
            var newsVM = new List<NewsDisplayViewModel>();
            foreach(var n in news)
            {
                newsVM.Add(new NewsDisplayViewModel { Id = n.Id, Time = n.Time.ToString(), Title = n.Title, Text = n.Text });
            }
            return View("Index",newsVM);
        }
    }
}