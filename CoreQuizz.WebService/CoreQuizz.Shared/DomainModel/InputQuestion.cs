namespace CoreQuizz.Shared.DomainModel
{
    public class InputQuestion : Question
    {
        public string Value { get; set; }

        public InputQuestion() : base()
        {
            this.QuestionType = QuestionType.Input;
        }
    }
}