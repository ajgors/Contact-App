﻿using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }

        public ContactDetails ContactDetails { get; set; }
    }
}
