using System.ComponentModel.DataAnnotations;

namespace CoreQuizz.WebService.Communication.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Password2 { get; set; }
    }
}
