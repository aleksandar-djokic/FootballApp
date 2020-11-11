using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class TeamJoinRequests
    {
        [Key]
        public int RequestId { get; set; }

        public string RequestInitiator { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Team Team { get; set; }

    }
}
