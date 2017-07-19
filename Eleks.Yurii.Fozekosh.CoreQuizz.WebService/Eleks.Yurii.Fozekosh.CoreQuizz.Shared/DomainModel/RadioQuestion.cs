using System.Collections.Generic;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class RadioQuestion : Question
    {
        public IList<QuestionOption> Options { get; set; }
    }
}