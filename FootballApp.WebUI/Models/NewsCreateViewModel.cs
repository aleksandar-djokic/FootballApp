using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class NewsCreateViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title must be under 50 characters long!")]
        public string Title { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Text must be under 250 characters long!")]
        public string Text { get; set; }
    }
}