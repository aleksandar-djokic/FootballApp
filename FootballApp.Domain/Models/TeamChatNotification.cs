using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("TeamChatNotifications")]
    public class TeamChatNotification:TeamNotification
    {
        [ForeignKey("TeamMessage")]
        public int TeamMessageId { get; set; }
        public virtual TeamChatMessage TeamMessage { get; set; }
    }
}
