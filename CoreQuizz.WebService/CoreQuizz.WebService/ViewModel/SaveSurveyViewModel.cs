using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.WebService.ViewModel
{
    public class SaveSurveyViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Description { get; set; }
        public int Access { get; set; }
        public IList<Question> QuestionDefinitions { get; set; }
    }
}