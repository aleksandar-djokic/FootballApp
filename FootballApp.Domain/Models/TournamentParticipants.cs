using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class TournamentParticipants
    {
        [Column(Order = 0), Key, ForeignKey("Tournament")]
        public int TournamentId { get; set; }
        [Column(Order = 1), Key, ForeignKey("Team")]
        public int TeamId { get; set; }
        public Tournament Tournament { get; set; }
        public virtual Team Team { get; set; }
        
    }
}
