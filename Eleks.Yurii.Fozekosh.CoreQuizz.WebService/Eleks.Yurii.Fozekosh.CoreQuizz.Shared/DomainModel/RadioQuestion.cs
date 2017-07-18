using System.Collections.Generic;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class RadioQuestion : Question
    {
        public int SelectedId { get; set; }
        public IList<RadioQuestion> Questions { get; set; }
    }
}