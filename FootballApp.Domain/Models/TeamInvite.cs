using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballApp.Domain.Models
{
    public class TeamInvite
    {
        [Key]
        public int InviteId { get; set; }

        [ForeignKey("Inviter")]
        public string InviterId { get; set; }

        [ForeignKey("Invitee")]
        public string InviteeId { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
        public virtual ApplicationUser Inviter { get; set; }
        public virtual ApplicationUser Invitee { get; set; }
    }
}
