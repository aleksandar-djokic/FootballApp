using FootballApp.WebUI.CustomValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FootballApp.WebUI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage="{0} je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} je obavezna.")]
        [DataType(DataType.Password)]
        [Display(Name = "Šifra")]
        public string Password { get; set; }

        [Display(Name = "Upamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {   
        [Required(ErrorMessage ="{0} je obavezno.")]
        [MaxLength(50,ErrorMessage ="Korisničko ime mora biti kraće od 50 karaktera.")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }
       

        [ValidateImage(ErrorMessage = "Molimo vas odaberite  PNG ili JPEG sliku manju od 4MB")]
        public HttpPostedFileBase File { get; set; }

        [Required(ErrorMessage = "{0} je obavezno.")]
        [EmailAddress(ErrorMessage ="Email nije validnog formata.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} je obavezna.")]
        [StringLength(100, ErrorMessage = "{0} mora biti najmanje {2} karaktera dugačka.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Šifra")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi šifru")]
        [Compare("Password", ErrorMessage = "Šifra i potvrda se ne poklapaju.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    
}
