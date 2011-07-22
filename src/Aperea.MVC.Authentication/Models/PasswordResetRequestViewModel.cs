﻿using System.ComponentModel.DataAnnotations;

namespace Aperea.MVC.Authentication.Models
{
    public class PasswordResetRequestViewModel
    {
        [Required]
        [StringLength(256)]
        public string EMail { get; set; }
    }
}