using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public abstract class Notification
    {
        [Key]
        public int Id { get; set; }
        public bool isRead { get; set; } = false;
        [ForeignKey("Reciever")]
        public string RecieverId { get; set; }

        public virtual ApplicationUser Reciever { get; set; }
        
    }
}
