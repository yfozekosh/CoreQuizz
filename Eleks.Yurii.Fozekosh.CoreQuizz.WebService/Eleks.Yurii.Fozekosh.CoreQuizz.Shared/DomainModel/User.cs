using System.ComponentModel.DataAnnotations;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class User
    {
        [Required()]
        public string Name { get; set; }
        
        [Required()]
        public string SecondName { get; set; }
 
        public string MiddleName { get; set; } = null;
        
        public string FullName => $"{Name} {MiddleName} {SecondName}";
        
        public string Email { get; set; } 
    }
}