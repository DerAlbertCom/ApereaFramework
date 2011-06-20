using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Aperea.MVC.Membership.Areas.Annotations;

namespace Aperea.MVC.Membership.Areas.Model
{
    public class PasswordResetViewModel
    {
        [Required]
        [StringLength(128)]
        [HiddenInput]
        public string Username { get; set; }

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