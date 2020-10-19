using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class Friendship
    {
        [Column(Order = 0), Key, ForeignKey("User1")]
        public string User1Id { get; set; }
        [Column(Order = 1), Key, ForeignKey("User2")]
        public string User2Id { get; set; }

        public virtual ApplicationUser User1 { get; set; }
        public virtual ApplicationUser User2 { get; set; }

    }
}
