﻿using System.ComponentModel.DataAnnotations;

namespace OutpatientClinic.Presentation.Models
{
    public class SignUpViewModel
    {


        [Required, Display(Name = "Username")]
        public string Username { get; set; }

        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
