using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class FriendsRepository : IFriendsRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        public IEnumerable<ApplicationUser> GetFriends(string id)
        {
            List<ApplicationUser> friends = new List<ApplicationUser>();
            var friendships = context.Friendships.Where(x => x.User1Id == id || x.User2Id == id).ToList();
            foreach(var f in friendships)
            {
                if (f.User1Id == id)
                {
                    friends.Add(f.User2);
                }
                else if (f.User2Id == id)
                {
                    friends.Add(f.User1);
                }
            }
            return friends;
        }
        public IEnumerable<FriendshipRequest> GetFriendRequests(string id)
        {

            var result = context.FriendRequests.Where(x => x.RequesterId == id || x.AddresseeId == id).ToList();
            //read friend notifications
            var notification = context.Notifications.OfType<FriendRequestNotification>().Where(x => x.isRead == false && x.RecieverId==id).ToList();
            foreach(var n in notification)
            {
                if (ObjectContext.GetObjectType(n.GetType()) == typeof(FriendRequestNotification))
                {
                    n.isRead = true;
                }
                
            }
            context.SaveChanges();
            return result;
        }
        public bool SendFriendRequest(string user1Id, string userName, out string resultmsg)
        {
            var result = false;
            resultmsg = "";
            try
            {
                var user1 = context.Users.First(x => x.Id == user1Id);
                var user2 = context.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());
                
              
                if (user2 == null)
                {
                    resultmsg = "User with entered username not found.";
                    return result;
                   
                }
                else if (user2.Id == user1.Id)
                {
                    resultmsg = "You cant add yourself as friend.";
                    return result;

                }
                var friendship = context.Friendships.Where(x => (x.User1Id == user1.Id && x.User2Id == user2.Id) || (x.User1Id == user2.Id && x.User2Id == user1.Id)).FirstOrDefault();
                var friendrequest = context.FriendRequests.Where(x => (x.RequesterId == user1.Id && x.AddresseeId == user2.Id) || (x.RequesterId == user2.Id && x.AddresseeId == user1.Id)).FirstOrDefault();
                if (friendship != null)
                {
                    resultmsg = "You are already friends with that person.";
                }
                else if(friendrequest != null)
                {
                    resultmsg = "Friend request already exists check Pending tab.";
                }
                else
                {
                    var fr = new FriendshipRequest { RequesterId = user1.Id, AddresseeId = user2.Id, Requester = user1, Addressee = user2 };
                    context.FriendRequests.Add(fr);
                    context.SaveChanges();
                    var notification = new FriendRequestNotification { RecieverId = user2.Id, isRead = false, FriendRequestId = fr.RequestId };
                    context.Notifications.Add(notification);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                resultmsg = "Something went wrong";
                result = false;
            }
            finally
            {
                context.SaveChanges();
            }
            return result;
        }
        public bool AddFriend(string currentUserId,string requestUserId)
        {
            var result = false;
            var currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
            var requestUser = context.Users.FirstOrDefault(x => x.Id == requestUserId);
            var friendrequest = context.FriendRequests.Where(x => (x.RequesterId == currentUser.Id && x.AddresseeId == requestUser.Id) || (x.RequesterId == requestUser.Id && x.AddresseeId == currentUser.Id)).FirstOrDefault();
            try
            {
                context.Friendships.Add(new Friendship { User1Id = requestUserId, User2Id = currentUserId, User1 = requestUser, User2 = currentUser });
                context.FriendRequests.Remove(friendrequest);
                context.SaveChanges();
                result = true;
            }
            catch(Exception)
            {
                result= false;
            }
            return result;
        }
        public bool DeclineFriendRequest(string currentUserId, string requestUserId)
        {
            var result = false;
            var currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
            var requestUser = context.Users.FirstOrDefault(x => x.Id == requestUserId);
            var friendrequest = context.FriendRequests.Where(x => (x.RequesterId == currentUser.Id && x.AddresseeId == requestUser.Id) || (x.RequesterId == requestUser.Id && x.AddresseeId == currentUser.Id)).FirstOrDefault();

            try
            {
                context.FriendRequests.Remove(friendrequest);
                context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public bool CancelFriendRequest(string currentUserId, string requestUserId)
        {
            var result = false;
            var currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
            var requestUser = context.Users.FirstOrDefault(x => x.Id == requestUserId);
            var friendrequest = context.FriendRequests.Where(x => (x.RequesterId == currentUser.Id && x.AddresseeId == requestUser.Id) || (x.RequesterId == requestUser.Id && x.AddresseeId == currentUser.Id)).FirstOrDefault();

            try
            {
                context.FriendRequests.Remove(friendrequest);
                context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;

        }

        public bool RemoveFriend(string currentUserId, string friendUserId)
        {
            var result = false;
            var currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
            var friendUser = context.Users.FirstOrDefault(x => x.Id == friendUserId);
            try
            {
                var friendship = context.Friendships.Where(x => (x.User1Id == currentUser.Id && x.User2Id == friendUser.Id) || (x.User1Id == friendUser.Id && x.User2Id == currentUser.Id)).FirstOrDefault();
                context.Friendships.Remove(friendship);
                context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
