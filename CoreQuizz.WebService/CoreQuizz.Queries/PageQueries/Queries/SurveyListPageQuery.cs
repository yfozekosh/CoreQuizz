using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Responces;

namespace CoreQuizz.Queries.PageQueries.Queries
{
    public class SurveyListPageQuery : IQuery<SurveyListItem[]>
    {
        public int UserId { get; set; }
        public string SearchText { get; set; }
    }
}