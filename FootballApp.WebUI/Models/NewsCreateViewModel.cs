using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class NewsCreateViewModel
    {
        [Required(ErrorMessage ="Naslov je obavezan.")]
        [StringLength(50, ErrorMessage = "Naslov mora biti kraći od 50 karaktera!")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Tekst je obavezan.")]
        [StringLength(250, ErrorMessage = "Tekst mora biti kraći od 250 karaktera!")]
        public string Text { get; set; }
    }
}