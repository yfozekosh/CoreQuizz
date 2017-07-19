namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class QuestionOption : BaseEntity
    {
        public string Value { get; set; }
        public bool? IsSelected { get; set; }

        public Question Question { get; set; }
    }
}