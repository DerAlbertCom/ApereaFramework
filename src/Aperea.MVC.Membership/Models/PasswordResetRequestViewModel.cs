using System.ComponentModel.DataAnnotations;

namespace Aperea.MVC.Areas.Membership.Models
{
    public class PasswordResetRequestViewModel
    {
        [Required]
        [StringLength(256)]
        public string EMail { get; set; }
    }
}