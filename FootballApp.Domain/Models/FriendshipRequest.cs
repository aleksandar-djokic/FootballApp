using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Models
{
    public class FriendshipRequest
    {
        [Key]
        public int RequestId { get; set; }

        [ForeignKey("Requester")]
        public string RequesterId { get; set; }

        [ForeignKey("Addressee")]
        public string AddresseeId { get; set; }

        public virtual ApplicationUser Requester { get; set; }
        public virtual ApplicationUser Addressee { get; set; }
    }
}
