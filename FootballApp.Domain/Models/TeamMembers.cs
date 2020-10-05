using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class TeamMembers
    {
        [Column(Order = 0), Key, ForeignKey("Team")]
        public int TeamId { get; set; }
        [Column(Order = 1), Key, ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("TeamRole")]
        public int RoleId { get; set; }

        public virtual TeamRole TeamRole { get; set; }
        public virtual Team Team { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
