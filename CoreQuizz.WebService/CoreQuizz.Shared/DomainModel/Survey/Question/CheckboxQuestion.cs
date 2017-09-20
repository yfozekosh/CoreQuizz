using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel.Survey.Question
{
    public class CheckboxQuestion : Abstract.Question
    {
        public IList<QuestionOption> Options { get; set; }
    }
}