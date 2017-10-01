﻿using System.ComponentModel.DataAnnotations;

namespace CoreQuizz.WebService.Communication.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
