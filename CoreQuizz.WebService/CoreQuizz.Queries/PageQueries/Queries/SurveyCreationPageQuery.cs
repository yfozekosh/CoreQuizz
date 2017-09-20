using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.PageQueries.Responces;

namespace CoreQuizz.Queries.PageQueries.Queries
{
    public class SurveyCreationPageQuery : IQuery<SurveyPageResult>
    {
        public int SurveyId { get; set; }
    }
}