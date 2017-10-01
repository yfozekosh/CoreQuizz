using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel.Abstract;
using CoreQuizz.Shared.DomainModel.Group;

namespace CoreQuizz.Shared.DomainModel.Survey
{
    public class Survey : ModifiableBaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public IList<Question.Abstract.Question> Questions { get; set; }

        public User CreatedBy { get; set; }
        
        public IList<SurveyStar> Stars { get; set; }
        
        public SurveyPassAccessLevel SurveyPassAccessLevel { get; set; }
        
        public CustomGroup GroupBelonged { get; set; }
        
        public IList<SurveyGrant> Grants { get; set; }
    }
}