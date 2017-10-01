namespace CoreQuizz.Shared.DomainModel.Survey.Question
{
    public class InputQuestion : Abstract.Question
    {
        public string Value { get; set; }

        public override string Type => "input";
    }
}