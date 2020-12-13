using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface IPrivateChatRepository
    {
        IEnumerable<Conversation> GetConversations(string userId);
        IEnumerable<Conversation> GetNotEmptyConversations(string userId);
        bool AreFriends(int conversationId);
        ApplicationUser ReturnFriend(string myId,int conversationId);
        ApplicationUser GetUser(string id);
        bool AddMessage(string userId, int conversationId, string Message, DateTime time);
        IEnumerable<PrivateMessage> GetMessages(int? messageCount, int conversationId);
        PrivateMessage getLastMsg(int conversationId);
        IEnumerable<ApplicationUser> getFriends(string userId,string toSearch);
        int GetConversationId(string myId, string recieverId);
        void readMessages(string userId, int conversationId);
        Conversation GetConversation(int conversationId);
    }
}
