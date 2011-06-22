using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Aperea.MVC.Authentication.Annotations;

namespace Aperea.MVC.Authentication.Models
{
    public class RegisterLoginViewModel
    {
        [Required]
        [StringLength(128)]
        [LabelName("Username")]
        public string LoginName { get; set; }

        [Required]
        [StringLength(256)]
        [LabelName("EMail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required]
        [StringLength(1024)]
        [LabelName("Password")]
        [DataType(DataType.Password)]
        [AllowHtml]
        public string Password { get; set; }

        [Required]
        [StringLength(1024)]
        [Compare("Password", ErrorMessageResourceType = typeof (MvcResourceStrings),
            ErrorMessageResourceName = "Error_The_password_and_confirmation_password_do_not_match")]
        [LabelName("ConfirmPassword")]
        [DataType(DataType.Password)]
        [AllowHtml]
        public string ConfirmPassword { get; set; }
    }
}