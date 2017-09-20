using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.PageQueries.Handlers.Abstract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class SurveySerachPageQueryHandler : EfQueryHandler<SurveySearchPageQuery, SurveyListItem[]>
    {        
        public SurveySerachPageQueryHandler(SurveyContext context) : base(context)
        {
        }
        
        public override async Task<QueryResult<SurveyListItem[]>> ExecuteAsync(SurveySearchPageQuery query)
        {
            var surveyQuery = Context.Surveys
                .Include(s=>s.CreatedBy)
                .Where(s => s.Title.Contains(query.SearchText))
                .OrderBy(x => x.ModifieDateTime)
                .Skip(query.PageCount * query.PageNumber)
                .Take(query.PageCount);

            SurveyListItem[] result = await surveyQuery.Select(survey => new SurveyListItem()
            {
                SurveyId = survey.Id,
                QuestionsCount = survey.Questions.Count,
                CreatedDate = survey.CreatedDate,
                ModifiedDate = survey.ModifieDateTime,
                Stars = 0,
                SurveyName = survey.Title,
                CreatedBy = survey.CreatedBy.Email
            }).ToArrayAsync();

            return new OkQueryResult<SurveyListItem[]>(result);
        }
    }
}