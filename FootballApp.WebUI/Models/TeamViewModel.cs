using FootballApp.WebUI.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} je obavezno.")]
        [MaxLength(50, ErrorMessage = "Ime mora biti kraće od 50 karaktera.")]
        [Display(Name = "Ime")]
        public string TeamName { get; set; }
        
        [Required(ErrorMessage = "{0} je obavezno.")]
        [MaxLength(250, ErrorMessage = "Opis mora biti kraći od 250 karaktera.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [ValidateImage(ErrorMessage = "Molimo vas izaberite PNG or JPEG sliku manju od 4MB")]
        [Display(Name = "Slika")]
        public HttpPostedFileBase Picture { get; set; }
    }
}