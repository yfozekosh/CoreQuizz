using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Handlers.Abstract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class SurveyListPageQueryHandler : EfQueryHandler<SurveyListPageQuery, SurveyListItem[]>
    {
        public SurveyListPageQueryHandler(SurveyContext context) : base(context)
        {
        }
        
        public override Task<SurveyListItem[]> ExecuteAsync(SurveyListPageQuery query)
        {
            IQueryable<Survey> usersSureveys = Context.Surveys.Where(survey => survey.CreatedBy.Id == query.UserId);

            Task<SurveyListItem[]> result = usersSureveys.Select(survey => new SurveyListItem()
            {
                SurveyId = survey.Id,
                QuestionsCount = survey.Questions.Count,
                CreatedDate = survey.CreatedDate,
                ModifiedDate = survey.ModifieDateTime,
                Stars = 0,
                SurveyName = survey.Title
            }).ToArrayAsync();

            return result;
        }
    }
}