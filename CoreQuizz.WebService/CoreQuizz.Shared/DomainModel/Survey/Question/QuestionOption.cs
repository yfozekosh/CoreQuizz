using CoreQuizz.Shared.DomainModel.Abstract;

namespace CoreQuizz.Shared.DomainModel.Survey.Question
{
    public class QuestionOption : ModifiableBaseEntity
    {
        public string Value { get; set; }
        
        public bool? IsSelected { get; set; }

        public Abstract.Question Question { get; set; }
    }
}