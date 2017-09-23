using CoreQuizz.Shared.DomainModel.Abstract;
using CoreQuizz.Shared.DomainModel.Enum;

namespace CoreQuizz.Shared.DomainModel.Survey
{
    public class SurveyGrant: ModifiableBaseEntity
    {
        public Survey Survey { get; set; }
        
        public User GrantedUser { get; set; }
        
        public SurveyEditAccessLevel AccessLevel { get; set; }
    }
}