using System.Collections.Generic;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class CheckboxQuestion : Question
    {
        public IList<CheckboxQuestion> Questions { get; set; }
    }
}