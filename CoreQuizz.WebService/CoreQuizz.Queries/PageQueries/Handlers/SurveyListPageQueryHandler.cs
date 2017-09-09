using System.Linq;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class SurveyListPageQueryHandler : IQueryHandler<SurveyListPageQuery, SurveyListItem[]>
    {
        private readonly SurveyContext _context;

        public SurveyListPageQueryHandler(SurveyContext context)
        {
            _context = context;
        }

        public SurveyListItem[] Execute(SurveyListPageQuery query)
        {
            IQueryable<Survey> usersSureveys = _context.Surveys.Where(survey => survey.CreatedBy.Id == query.UserId);

            SurveyListItem[] result = usersSureveys.Select(survey => new SurveyListItem()
            {
                SurveyId = survey.Id,
                QuestionsCount = survey.Questions.Count,
                CreatedDate = survey.CreatedDate,
                ModifiedDate = survey.ModifieDateTime,
                Stars = 0,
                SurveyName = survey.Title
            }).ToArray();

            return result;
        }
    }
}