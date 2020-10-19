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
                friendList.Add(new FriendViewModel { Name = f.UserName, ImageSource = imagesource });
            }
            return View(friendList);
        }
    }
}