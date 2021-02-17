using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? NumberOfParticipants { get; set; }
        public int? NumberOfRounds { get; set; }
        public int? CurrentRound { get; set; }
        public bool isActive { get; set; }
        [ForeignKey("Winner")]
        public int? WinnerId { get; set; }
        [ForeignKey("RunnerUp")]
        public int? RunnerUpId { get; set; }
        public virtual Team Winner { get; set; }
        public virtual Team RunnerUp { get; set; }

    }
}
