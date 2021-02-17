using FootballApp.Domain.Abstract;
using FootballApp.WebUI.Hubs;
using FootballApp.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballApp.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class PrivateChatController : Controller
    {
        public IPrivateChatRepository chat;

        public PrivateChatController(IPrivateChatRepository chatRepository)
        {
            this.chat = chatRepository;

        }
        // GET: PrivateChat
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var Conversations = chat.GetNotEmptyConversations(userId).ToList();
            var result = new List<ConversationViewModel>();
            for(int i = 0; i < Conversations.Count(); i++)
            {
                var current = Conversations[i];
                var lastMsg = chat.getLastMsg(current.Id);
                if (lastMsg != null)
                {
                     var vm = new ConversationViewModel { Id = current.Id,Time=lastMsg.Time.ToString(),Message=lastMsg.Message};

                    if (lastMsg.UserId == userId)
                    {
                        var user= chat.GetUser(userId);
                        var friend = chat.ReturnFriend(userId, current.Id);
                        vm.UserName = friend.UserName;
                        vm.MessageSender = user.UserName;
                        vm.isRead = true;
                        var imagesource = "";
                        if (friend.ProfilePicture != null)
                        {
                            string imageBase64 = Convert.ToBase64String(friend.ProfilePicture);
                            imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                        }
                        vm.ImageSource = imagesource;
                    }
                    else
                    {
                        var user=chat.GetUser(lastMsg.UserId);
                        vm.UserName = user.UserName;
                        vm.MessageSender = user.UserName;
                        vm.isRead = lastMsg.isRead;
                        var imagesource = "";
                        if (user.ProfilePicture != null)
                        {
                            string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                            imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                        }
                        vm.ImageSource = imagesource;
                    }
                    result.Add(vm);
                }
               




            }
            result.OrderByDescending(x => x.isRead).ThenBy(x => x.Time);
            ViewBag.myName = chat.GetUser(userId).UserName;
            //Create View add result to view
            return View(result.OrderBy(x => x.isRead).ThenByDescending(x => x.Time));
        }
        [HttpGet]
        public JsonResult GetConversation(int conversationId)
        {
            var userId = User.Identity.GetUserId();
            var conversation = chat.GetConversation(conversationId);
            var result = new ConversationViewModel();
            var lastMsg = chat.getLastMsg(conversation.Id);
            if (lastMsg != null)
            {
                result.Id = conversation.Id;
                result.Time = lastMsg.Time.ToString();
                result.Message = lastMsg.Message;

                if (userId == lastMsg.UserId)
                {
                    var user = chat.GetUser(userId);
                    var friend = chat.ReturnFriend(userId, conversation.Id);
                    result.UserName = friend.UserName;
                    result.MessageSender = user.UserName;
                    result.isRead = true;
                    var imagesource = "";
                    if (friend.ProfilePicture != null)
                    {
                        string imageBase64 = Convert.ToBase64String(friend.ProfilePicture);
                        imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                    }
                    result.ImageSource = imagesource;
                }
                else
                {
                    var user = chat.GetUser(lastMsg.UserId);
                    result.UserName = user.UserName;
                    result.MessageSender = user.UserName;
                    result.isRead = lastMsg.isRead;
                    var imagesource = "";
                    if (user.ProfilePicture != null)
                    {
                        string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                        imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                    }
                    result.ImageSource = imagesource;
                }
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetChat(string id)
        {
            var userId = User.Identity.GetUserId();
            var conversationId = chat.GetConversationId(userId, id);
            return RedirectToAction("Chat", new { conversationId = conversationId});
        }
        public ActionResult Chat(int conversationId)
        {
            var userId = User.Identity.GetUserId();
            var user = chat.GetUser(userId);
            var friend = chat.ReturnFriend(userId,conversationId);
            var areFriends = chat.AreFriends(conversationId);
            var imagesource = "";
            if (friend.ProfilePicture != null)
            {
                string imageBase64 = Convert.ToBase64String(friend.ProfilePicture);
                imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
            }
            var myImagesource = "";
            if (user.ProfilePicture != null)
            {
                string imageBase64 = Convert.ToBase64String(user.ProfilePicture);
                myImagesource = string.Format("data:image/png;base64,{0}", imageBase64);
            }
            chat.readMessages(userId,conversationId);
            return View(new ChatViewModel {Id=conversationId,myName=user.UserName, areFriends=areFriends,UserName=friend.UserName,myImageSource=myImagesource,ImageSource=imagesource });
        }
        [HttpPost]
        public JsonResult SendMsg(string message, string recieverName, int conversationId)
        {
            var userId = User.Identity.GetUserId();
            var sender = chat.GetUser(userId);
            var result = chat.AddMessage(userId, conversationId, message, DateTime.Now);
            if (result)
            {
                NotificationHub.SendChatNotification(conversationId, recieverName);
                PrivateChatHub.Send(sender.UserName,recieverName, message, DateTime.Now.ToString(), conversationId);
            }

            return Json(result);
        }
        [HttpGet]
        public JsonResult GetMessages(int? messageCount,int conversationId)
        {
            var messages = chat.GetMessages(messageCount, conversationId).ToList();
            var result = new List<PrivateChatMessageViewModel>();
            for (int i = 0; i < messages.Count(); i++)
            {
                var current = messages[i];
                string imagesource = "";
                if (current.User.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(current.User.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                result.Add(new PrivateChatMessageViewModel
                {
                    UserName = current.User.UserName,
                    Time = current.Time.ToString(),
                    Message = current.Message,
                    ImageSource = imagesource
                });
            }
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public JsonResult GetFriend(string toSearch)
        {
            var userId = User.Identity.GetUserId();
            var friends = chat.getFriends(userId,toSearch);
            var result = new List<FriendViewModel>();
            foreach(var f in friends)
            {
                var imagesource = "";
                if (f.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(f.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                result.Add(new FriendViewModel { Id = f.Id, Name = f.UserName, ImageSource = imagesource });
            }
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult ReadMsg(int conversationId)
        {
            var userId = User.Identity.GetUserId();
            chat.readMessages(userId, conversationId);
            return Json(true);
        }
    }
}     