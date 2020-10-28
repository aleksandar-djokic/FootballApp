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
    public class FriendsController : Controller
    {
        public IFriendsRepository friends;

        public FriendsController(IFriendsRepository friendRepository)
        {
            this.friends = friendRepository;

        }
        // GET: Friends
        public ActionResult Index()
        {
            List<FriendViewModel> friendList = new List<FriendViewModel>();
            var userid = User.Identity.GetUserId();
            var userFriends=friends.GetFriends(userid);
            foreach(var f in userFriends)
            {
                string imagesource = "";
                if (f.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(f.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                }
                friendList.Add(new FriendViewModel { Id=f.Id,Name = f.UserName, ImageSource = imagesource });
            }
            return View(friendList);
        }
        public JsonResult GetFriends()
        {
            List<FriendViewModel> result = new List<FriendViewModel>();
            var userid = User.Identity.GetUserId();
            var userFriends = friends.GetFriends(userid);
            foreach (var f in userFriends)
            {
                string imagesource = "";
                if (f.ProfilePicture != null)
                {
                    string imageBase64 = Convert.ToBase64String(f.ProfilePicture);
                    imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                }
                result.Add(new FriendViewModel { Id = f.Id, Name = f.UserName, ImageSource = imagesource });
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult AddFriend(string userName)
        {
            var resultmsg = "";
            var resultvalue = false;
            var userid = User.Identity.GetUserId();
            if (friends.SendFriendRequest(userid, userName,out resultmsg))
            {
                resultvalue = true;
            }
            var result = new { resultvalue = resultvalue, resultmsg = resultmsg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetRequests()
        {
            var userid = User.Identity.GetUserId();
            var requests = friends.GetFriendRequests(userid);
            var resultitems = new List<FriendRequestViewModel>();
            var resultmsg = "";
            if (requests.Count() > 0)
            {
                foreach(var r in requests)
                {
                    var imagesource = "";
                    FriendRequestViewModel temp = new FriendRequestViewModel();
                    if (r.Requester.Id == userid)
                    {
                        temp.Id = r.AddresseeId;
                        temp.Name = r.Addressee.UserName;
                        temp.Direction = "Outgoing";
                        if (r.Addressee.ProfilePicture != null)
                        {
                            string imageBase64 = Convert.ToBase64String(r.Addressee.ProfilePicture);
                            imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                        }
                        temp.ImageSource = imagesource;
                    }
                    else
                    {
                        temp.Id = r.RequesterId;
                        temp.Name = r.Requester.UserName;
                        temp.Direction = "Incoming";
                        if (r.Requester.ProfilePicture != null)
                        {
                            string imageBase64 = Convert.ToBase64String(r.Requester.ProfilePicture);
                            imagesource = string.Format("data:image/png;base64,{0}", imageBase64);

                        }
                        temp.ImageSource = imagesource;

                    }
                    resultitems.Add(temp);
                 }
                resultmsg = "full";
            }
            else{
                    resultmsg = "empty";
                }
            var result = new { resultitems = resultitems, resultmsg = resultmsg };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AcceptFriendRequest(string requestUserId)
        {
            var result = false;
            var userId = User.Identity.GetUserId();
            result = friends.AddFriend(userId, requestUserId);
            return Json(result);
        }
        [HttpPost]
        public JsonResult DeclineFriendRequest(string requestUserId)
        {
            var result = false;
            var userId = User.Identity.GetUserId();
            result = friends.DeclineFriendRequest(userId, requestUserId);
            return Json(result);
        }
        [HttpPost]
        public JsonResult CancelFriendRequest(string requestUserId)
        {
            var result = false;
            var userId = User.Identity.GetUserId();
            result = friends.CancelFriendRequest(userId, requestUserId);
            return Json(result);
        }
        [HttpPost]
        public JsonResult RemoveFriend(string friendUserId)
        {
            var result = false;
            var userId = User.Identity.GetUserId();
            result = friends.RemoveFriend(userId, friendUserId);
            return Json(result);
        }
    }
}