using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    public class NewsController : Controller
    {
        public INewsRepository news;
        public NewsController (INewsRepository newsRepository)
        {
            this.news = newsRepository;
        }
        // GET: News
        public ActionResult Index()
        {
            var newsList = news.GetNews().ToList();
            var viewModels = new List<NewsDisplayViewModel>();
            foreach(var n in newsList)
            {
                viewModels.Add(new NewsDisplayViewModel { Id = n.Id, Title = n.Title, Text = n.Text, Time = n.Time.ToString() });
            }
            return View(viewModels);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult Create(NewsCreateViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                news.Create(ViewModel.Title, ViewModel.Text);
                return View();
            }
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var News = news.GetNewsById(Id);
            var ViewModel = new NewsEditViewModel
            {
                Id = News.Id,
                Title = News.Title,
                Text = News.Text,
                Time = News.Time
            };
            return View(ViewModel);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult Edit(NewsEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                news.Edit(vm.Id, vm.Title, vm.Text);
                vm.Time = DateTime.Now;
                return View(vm);
            }
            vm.Time = DateTime.Now;
            return View(vm);
        }
    }
}