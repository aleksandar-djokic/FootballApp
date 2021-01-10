using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    [Table("TeamMatchNotifications")]
    public class TeamMatchNotification : TeamNotification
    {
        public string Type { get; set; }
        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public virtual Match Match { get; set; } 
    }
}
