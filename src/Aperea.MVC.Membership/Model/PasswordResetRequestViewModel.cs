using System.ComponentModel.DataAnnotations;

namespace Aperea.MVC.Membership.Areas.Model
{
    public class PasswordResetRequestViewModel
    {
        [Required]
        [StringLength(256)]
        public string EMail { get; set; }
    }
}