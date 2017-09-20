using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Commands
{
    public class SaveQuestionsCommand
    {
        public IEnumerable<Question> Questions { get; set; }

        public int SurveyId { get; set; }
    }
}