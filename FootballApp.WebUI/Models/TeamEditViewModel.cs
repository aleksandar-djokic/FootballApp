using FootballApp.WebUI.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class TeamEditViewModel
    {   public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name must be under 50 characters long.")]
        [Display(Name = "TeamName")]
        public string TeamName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Description must be under 250 characters long.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [ValidateImage(ErrorMessage = "Please select a PNG or JPEG image smaller than 4MB")]
        [Display(Name = "Picture")]
        public HttpPostedFileBase Picture { get; set; }
    }
}