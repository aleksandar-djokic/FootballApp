using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class SupportRepository : ISupportRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public bool CloseTicket(int id)
        {
            var result = false;
            var ticket = context.Tickets.FirstOrDefault(x => x.Id == id);
            try
            {
                ticket.IsOpened = false;
                context.SaveChanges();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public int CreateTicket(string Title, string Topic, string Message, string userId)
        {
            var newTicket = new SupportTicket() { Title = Title, Topic = Topic, UserId = userId, IsOpened = true, Time = DateTime.Now };
            context.Tickets.Add(newTicket);
            context.SaveChanges();
            var newMessage = new TicketMessage { Message = Message, TicketId = newTicket.Id, Time = DateTime.Now ,UserId =userId};
            context.TicketMessages.Add(newMessage);
            context.SaveChanges();
            return newTicket.Id;
        }

        public IEnumerable<SupportTicket> GetAdminTickets()
        {
            var tickets = context.Tickets.Where(x => x.IsOpened == true).OrderBy(x => x.Time);
            return tickets;
        }

        public IEnumerable<TicketMessage> GetMessages(int id)
        {
            var messages = context.TicketMessages.Where(x => x.TicketId == id).ToList().OrderBy(x => x.Time);
            return messages;
        }

        public SupportTicket GetTicket(int ticketId)
        {
            var ticket = context.Tickets.FirstOrDefault(x => x.Id == ticketId);
            return ticket;
        }

        public IEnumerable<SupportTicket> GetTickets(string UserId)
        {
            var tickets = context.Tickets.Where(x => x.UserId == UserId).OrderBy(x=>x.IsOpened);
            return tickets;
        }

        public TicketMessage SendMessage(int id, string userId, string message,out bool result)
        {
            result = false;
            var ticket = context.Tickets.FirstOrDefault(x => x.Id == id);
            
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            var newMessage = new TicketMessage() { Message = message, Time = DateTime.Now, TicketId = id, UserId = userId,User=user };
            if (ticket.IsOpened)
            {
                try
                {
                
                    context.TicketMessages.Add(newMessage);
                    context.SaveChanges();
                    result = true;
                }
                catch
                {
                    result = false;
                }

            }
            return newMessage;
        }
    }
}
