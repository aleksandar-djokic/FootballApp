using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("TeamInviteNotifications")]
    public class TeamInviteNotification:TeamNotification
    {
        [ForeignKey("TeamInvite")]
        public int TeamInviteId {get; set; }
        public virtual TeamInvite TeamInvite { get; set; }
    }
}
