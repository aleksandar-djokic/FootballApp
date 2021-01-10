using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("TeamRequestNotifications")]
    public class TeamRequestNotification:TeamNotification
    {
        [ForeignKey("TeamJoinRequest")]
        public int RequestId { get; set; }
        public virtual TeamJoinRequests TeamJoinRequest { get; set; }
    }
}
