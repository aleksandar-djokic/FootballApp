using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballApp.Domain.Models;

namespace FootballApp.Domain.Abstract
{
    public interface IFriendsRepository
    {
        IEnumerable<ApplicationUser> GetFriends(string id);
        bool SendFriendRequest(string user1Id, string userName,out string resultmsg);
        IEnumerable<FriendshipRequest> GetFriendRequests(string id);
        bool AddFriend(string currentUserId, string requestUserId);
        bool RemoveFriend(string currentUserId, string friendUserId);
        bool DeclineFriendRequest(string currentUserId, string requestUserId);
        bool CancelFriendRequest(string currentUserId, string requestUserId);
    }
}
