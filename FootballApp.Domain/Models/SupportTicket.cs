using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class SupportTicket
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public bool IsOpened { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
