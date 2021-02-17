using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class TournamentMatch
    {
        [Key]
        public int Id { get; set; }
        public int Round { get; set; }
        public int? ScoreP1 { get; set; }
        public int? ScoreP2 { get; set; }
        public bool isConcluded { get; set; } = false;
        [ForeignKey("Participant1")]
        public int Participant1Id { get; set; }
        [ForeignKey("Participant2")]
        public int Participant2Id { get; set; }
        [ForeignKey("Tournament")]
        public int TournamentId { get; set; }
        [ForeignKey("Winner")]
        public int? WinnerId { get; set; }
        public virtual Team Participant1 { get; set; }
        public virtual Team Participant2 { get; set; }
        public virtual Team Winner { get; set; }
        public virtual Tournament Tournament { get; set; }

    }
}
