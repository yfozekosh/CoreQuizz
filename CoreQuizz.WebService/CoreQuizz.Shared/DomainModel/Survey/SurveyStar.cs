using CoreQuizz.Shared.DomainModel.Abstract;

namespace CoreQuizz.Shared.DomainModel.Survey
{
    public class SurveyStar : ModifiableBaseEntity
    {
        public Survey Survey { get; set; }
        
        public User LeftBy { get; set; }
    }
}