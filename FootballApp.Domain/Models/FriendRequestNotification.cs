using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("FriendNotifications")]
    public class FriendRequestNotification:Notification
    {
        [ForeignKey("FriendRequest")]
        public int FriendRequestId { get; set; }
        public virtual FriendshipRequest FriendRequest { get; set; }
    }
}
