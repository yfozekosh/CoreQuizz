using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel
{
    public class RadioQuestion : Question
    {
        public IList<QuestionOption> Options { get; set; }
    }
}