using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("PrivateChatNotifications")]
    public class PrivateChatNotification:Notification
    {
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public virtual PrivateMessage Message { get; set; }
    }
}
