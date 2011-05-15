using System.ComponentModel.DataAnnotations;

namespace ApereaStart.Models
{
    public class PasswordResetRequestViewModel 
    {
        [Required]
        [StringLength(256)]
        public string EMail { get; set; }
    }
}