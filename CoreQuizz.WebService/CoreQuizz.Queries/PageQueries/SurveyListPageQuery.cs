using CoreQuizz.DataAccess.Contract.Contracts;

namespace CoreQuizz.Queries.PageQueries
{
    public class SurveyListPageQuery : IQuery<SurveyListItem[]>
    {
        public int UserId { get; set; }
        public string SearchText { get; set; }
    }
}