using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class PrivateMessage
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Message { get; set; }
        public bool isRead { get; set; } = false;
        public DateTime Time { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}
