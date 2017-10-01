using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.PageQueries.Handlers.Abstract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.Shared.DomainModel.Survey;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class SurveyListPageQueryHandler : EfQueryHandler<SurveyListPageQuery, SurveyListItem[]>
    {
        public SurveyListPageQueryHandler(SurveyContext context) : base(context)
        {
        }
        
        public override async Task<QueryResult<SurveyListItem[]>> ExecuteAsync(SurveyListPageQuery query)
        {
            IQueryable<Survey> usersSureveys = Context.Surveys.Where(survey => survey.CreatedBy.Id == query.UserId);

            SurveyListItem[] result = await usersSureveys.Select(survey => new SurveyListItem()
            {
                SurveyId = survey.Id,
                QuestionsCount = survey.Questions.Count,
                CreatedDate = survey.CreatedDate,
                ModifiedDate = survey.ModifieDateTime,
                Stars = survey.Stars.Count,
                SurveyName = survey.Title,
                Description = survey.Description
            }).ToArrayAsync();

            return new OkQueryResult<SurveyListItem[]>(result);
        }
    }
}