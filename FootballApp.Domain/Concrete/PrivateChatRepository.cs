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
    public class PrivateChatRepository : IPrivateChatRepository
    {

        private ApplicationDbContext context = new ApplicationDbContext();

        public bool AreFriends(int conversationId)
        {
            var result = false;
            var conversation = context.Conversations.FirstOrDefault(x => x.Id == conversationId);
            var areFriends = context.Friendships.FirstOrDefault(x => (x.User1Id == conversation.User1Id && x.User2Id == conversation.User2Id) ||
              (x.User1Id == conversation.User2Id && x.User2Id == conversation.User1Id));
            if (areFriends != null)
            {
                result = true;
            }
            return result;
        }

        public IEnumerable<Conversation> GetConversations(string userId)
        {
            return context.Conversations.Where(x => x.User1Id == userId || x.User2Id == userId);
        }

        public ApplicationUser ReturnFriend(string myId, int conversationId)
        {
            var conversation = context.Conversations.FirstOrDefault(x => x.Id == conversationId);
            var friend = myId == conversation.User1Id ? conversation.User2 : conversation.User1;
            return friend;
        }

        public ApplicationUser GetUser(string id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool AddMessage(string userId, int conversationId, string Message, DateTime time)
        {
            var result = false;
            try
            {
                var message = new PrivateMessage { Message = Message, isRead = false, Time = time, UserId = userId, ConversationId = conversationId };
                context.PrivateMessages.Add(message);
                context.SaveChanges();
                var friend = ReturnFriend(userId, conversationId);
                context.Notifications.Add(new PrivateChatNotification { isRead=false,Reciever=friend,Message=message });
                context.SaveChanges();

                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public IEnumerable<PrivateMessage> GetMessages(int? messageCount, int conversationId)
        {
            var count = messageCount == null ? 0 : messageCount;
            var messages = context.PrivateMessages.Where(x => x.ConversationId == conversationId).OrderByDescending(x => x.Time).Skip(Convert.ToInt32(count)).Take(10);
            return messages;
        }

        public IEnumerable<Conversation> GetNotEmptyConversations(string userId)
        {
            var conversations = context.Conversations.Where(x => x.User1Id == userId || x.User2Id == userId).ToList();
            var result = new List<Conversation>();
            for (int i = 0; i < conversations.Count; i++)
            {
                var current = conversations[i];
                var lastmsg = context.PrivateMessages.OrderByDescending(x => x.Time).FirstOrDefault();
                if (lastmsg == null)
                {
                    continue;
                }
                result.Add(current);
            }
            return result;
        }

        public PrivateMessage getLastMsg(int conversationId)
        {
            return context.PrivateMessages.Where(x=>x.ConversationId==conversationId).OrderByDescending(x => x.Time).FirstOrDefault();
        }

        public IEnumerable<ApplicationUser> getFriends(string userId, string toSearch)
        {
            var friendships= context.Friendships.Where(x => (x.User1Id == userId && x.User2.UserName.ToLower().StartsWith(toSearch.ToLower())) || (x.User2Id == userId && x.User1.UserName.ToLower().StartsWith(toSearch.ToLower()))).ToList();
            var result = new List<ApplicationUser>();
            foreach(var f in friendships)
            {
                if (f.User1Id == userId)
                {
                    result.Add(f.User2);
                }
                else
                {
                    result.Add(f.User1);
                }
            }
            return result;
        }

        public int GetConversationId(string myId, string recieverId)
        {
            int conversationId;
            var conversation = context.Conversations.FirstOrDefault(x => (x.User1Id == myId && x.User2Id == recieverId) || (x.User2Id == myId && x.User1Id == recieverId));
            if (conversation != null)
            {
                conversationId = conversation.Id;
            }
            else
            {
                var newConversation = new Conversation { User1Id = myId, User2Id = recieverId };
                context.Conversations.Add(newConversation);
                context.SaveChanges();
                conversationId = newConversation.Id;
            }

            return conversationId;
        }

        public void readMessages(string userId, int conversationId)
        {
            var conversation = context.Conversations.FirstOrDefault(x => x.Id == conversationId);
            var friendId = userId == conversation.User1Id ? conversation.User2Id : conversation.User1Id;
            var messagesToRead = context.PrivateMessages.Where(x => x.ConversationId == conversationId && x.UserId == friendId && x.isRead == false);
            foreach (var m in messagesToRead)
            {
                m.isRead = true;

            }
            //read notifications
            var notification = context.Notifications.OfType<PrivateChatNotification>().Where(x => x.isRead == false && x.RecieverId == userId).ToList();
            foreach (var n in notification)
            {
                if (ObjectContext.GetObjectType(n.GetType()) == typeof(PrivateChatNotification))
                {
                    n.isRead = true;
                }

            }
            context.SaveChanges();
        }

        public Conversation GetConversation(int conversationId)
        {
            return context.Conversations.FirstOrDefault(x => x.Id == conversationId);
        }
    }
}
