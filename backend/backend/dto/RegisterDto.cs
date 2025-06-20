﻿using System.ComponentModel.DataAnnotations;

namespace backend.dto
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
