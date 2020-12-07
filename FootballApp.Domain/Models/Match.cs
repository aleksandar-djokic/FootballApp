using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Team1")]
        public int Team1Id { get; set; }
        [ForeignKey("Team2")]
        public int Team2Id { get; set; }
        public DateTime DateTime { get; set; }
        [DefaultValue(true)]
        public bool isAccepted { get; set; }
        public string Adress { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }

    }
}
