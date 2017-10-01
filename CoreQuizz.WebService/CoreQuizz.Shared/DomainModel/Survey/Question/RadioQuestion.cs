using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel.Survey.Question
{
    public class RadioQuestion : Abstract.Question
    {
        public IList<QuestionOption> Options { get; set; }
        
        public override string Type => "radio";
    }
}