namespace CoreQuizz.Shared.DomainModel
{
    public class SurveyStar : BaseEntity
    {
        public Survey Survey { get; set; }
        
        public User LeftBy { get; set; }
    }
}