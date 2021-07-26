using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class CreateTournamentViewModel
    {
        [Required(ErrorMessage ="Naziv je obavezno uneti")]
        [MaxLength(50)]
        public string Name { get; set; }
        public int NumberOfParticipants { get; set; }
    }
}