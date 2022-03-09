﻿using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email {  get; set; }
        
        [Required]
        public string Password {  get; set; }
    }
}