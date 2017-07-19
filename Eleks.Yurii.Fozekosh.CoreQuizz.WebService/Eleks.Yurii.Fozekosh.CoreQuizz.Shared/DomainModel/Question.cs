namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public abstract class Question : BaseEntity
    {
        public int? ResultId { get; set; }
        public string QuestionLabel { get; set; }
        public QuestionType QuestionType { get; set; }

        public virtual Survey Survey {get; set; }
    }
}
