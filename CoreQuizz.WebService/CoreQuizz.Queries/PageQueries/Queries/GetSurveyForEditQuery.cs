using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Responces;

namespace CoreQuizz.Queries.PageQueries.Queries
{
    public class GetSurveyForEditQuery : IQuery<SurveyDefinitionViewModel>
    {
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        
        public GetSurveyForEditQuery(int surveyId, int userId)
        {
            this.SurveyId = surveyId;
            UserId = userId;
        }
    }
}