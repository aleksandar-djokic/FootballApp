using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("TeamMemberNotifications")]
    public class TeamMemberNotification:TeamNotification
    {
        [ForeignKey("JoinRequest")]
        public int JoinRequestId { get; set; }
        public virtual TeamJoinRequests JoinRequest { get; set; }
    }
}
