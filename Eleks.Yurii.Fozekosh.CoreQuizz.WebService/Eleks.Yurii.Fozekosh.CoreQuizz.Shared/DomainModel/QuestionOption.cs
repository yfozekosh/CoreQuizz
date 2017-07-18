namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class QuestionOption : BaseEntity
    {
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }
}