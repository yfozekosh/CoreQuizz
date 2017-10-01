using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreQuizz.Shared.DomainModel.Abstract;
using CoreQuizz.Shared.DomainModel.Group;
using CoreQuizz.Shared.DomainModel.Survey;

namespace CoreQuizz.Shared.DomainModel
{
    public class User : ModifiableBaseEntity
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; } = null;

        public string FullName => $"{Name} {MiddleName} {SecondName}";

        [Required]
        public string Email { get; set; }

        public IList<Survey.Survey> Surveys { get; set; }
        
        public IList<CustomGroup> Groups { get; set; }
        
        public IList<SurveyStar> Stars { get; set; }
    }
}