using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel
{
    public class CheckboxQuestion : Question
    {
        public IList<QuestionOption> Options { get; set; }
    }
}