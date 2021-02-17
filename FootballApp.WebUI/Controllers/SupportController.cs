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
    [Authorize]
    public class SupportController : Controller
    {
        private ISupportRepository supportRepo;
        public SupportController(ISupportRepository repo)
        {
            this.supportRepo = repo;
        }
        // GET: Support
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Admin"))
            {
                var adminTicketList = new List<TicketAdminViewModel>();
                var adminTickets = supportRepo.GetAdminTickets().ToList();
                foreach (var t in adminTickets)
                {
                    var userName = t.User.UserName;
                    TicketAdminViewModel newAdminTicketVM = new TicketAdminViewModel { Id = t.Id, User = userName, Title = t.Title, Status = t.IsOpened };
                    adminTicketList.Add(newAdminTicketVM);
                }
              
                return View("AdminIndex",adminTicketList);
            }
            var ticketList = new List<TicketUserViewModel>();
            var tickets = supportRepo.GetTickets(userId);
            foreach(var t in tickets)
            {
                var currentTicket = new TicketUserViewModel { Id = t.Id, Status = t.IsOpened, Title = t.Title };
                ticketList.Add(currentTicket);
             }
            
            
            return View("Index", ticketList.OrderByDescending(x => x.Status));
        }
        public ActionResult Create(string Title,string Topic,string Message)
        {
            var userId = User.Identity.GetUserId();
            var ticketId = supportRepo.CreateTicket(Title, Topic, Message, userId);
            return RedirectToAction("ViewTicket",new { id = ticketId });
        }
        public ActionResult ViewTicket(int id)
        {
            var ticket = supportRepo.GetTicket(id);
            var ticketViewModel = new TicketViewModel() { Id=ticket.Id,Title=ticket.Title,Topic=ticket.Topic,Time=ticket.Time,isOpened=ticket.IsOpened,UserSender=ticket.User.UserName,messages=new List<TicketMessageViewModel>()};
            var ticketmessages = supportRepo.GetMessages(id);
            foreach (var t in ticketmessages)
            {
                var newMessageVM = new TicketMessageViewModel() { Message = t.Message, Time = t.Time, UserName = t.User.UserName };
                ticketViewModel.messages.Add(newMessageVM);
            }
            return View(ticketViewModel);
        }
        [HttpPost]
        public JsonResult SendMessage(int id,string message)
        {
            var result = false;
            var userId = User.Identity.GetUserId();
            var newMessage = supportRepo.SendMessage(id, userId, message, out result);
            var data = new { result = result, Name = newMessage.User.UserName, Time = newMessage.Time.ToString(), Message = newMessage.Message };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CloseTicket(int id)
        {
            var result = supportRepo.CloseTicket(id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}