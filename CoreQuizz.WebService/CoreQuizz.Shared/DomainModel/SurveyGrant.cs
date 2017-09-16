namespace CoreQuizz.Shared.DomainModel
{
    public class SurveyGrant: BaseEntity
    {
        public Survey Survey { get; set; }
        
        public User GrantedUser { get; set; }
        
        public SurveyEditAccessLevel AccessLevel { get; set; }
    }
}