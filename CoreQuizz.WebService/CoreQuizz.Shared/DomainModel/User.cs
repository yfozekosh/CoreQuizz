using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreQuizz.Shared.DomainModel
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; } = null;

        public string FullName => $"{Name} {MiddleName} {SecondName}";

        [Required]
        public string Email { get; set; }

        public IList<Survey> Surveys { get; set; }
    }
}