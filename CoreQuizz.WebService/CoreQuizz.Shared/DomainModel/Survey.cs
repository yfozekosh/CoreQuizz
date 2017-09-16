using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public IList<Question> Questions { get; set; }

        public User CreatedBy { get; set; }
        
        public IList<SurveyStar> Stars { get; set; }
        
        public SurveyPassAccessLevel SurveyPassAccessLevel { get; set; }
        
        public CustomGroup GroupBelonged { get; set; }
        
        public IList<SurveyGrant> Grants { get; set; }
    }
}