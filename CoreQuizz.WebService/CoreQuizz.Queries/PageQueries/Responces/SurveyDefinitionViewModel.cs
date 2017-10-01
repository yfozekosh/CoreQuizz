using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Queries.PageQueries.Responces
{
    public class SurveyDefinitionViewModel : SurveyViewModel
    {
        public IList<Question> Questions { get; set; }
    }
}