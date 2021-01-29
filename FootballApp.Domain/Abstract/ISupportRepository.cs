using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface ISupportRepository
    {
        IEnumerable<SupportTicket> GetTickets(string UserId);
        IEnumerable<SupportTicket> GetAdminTickets();
        IEnumerable<TicketMessage> GetMessages(int id);
        SupportTicket GetTicket(int ticketId);
        int CreateTicket(string Title, string Topic, string Message, string userId);
        TicketMessage SendMessage(int id, string userId, string message,out bool result);
        bool CloseTicket(int id);
    }
}
