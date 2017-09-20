using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.PageQueries.Handlers.Abstract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.Shared.DomainModel.Survey;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class SurveyCreationPageHandler : EfQueryHandler<SurveyCreationPageQuery, SurveyPageResult>
    {
        public SurveyCreationPageHandler(SurveyContext context) : base(context)
        {
        }

        public override async Task<QueryResult<SurveyPageResult>> ExecuteAsync(SurveyCreationPageQuery query)
        {
            Survey survey = await Context.Surveys.FindAsync(query.SurveyId);

            return new OkQueryResult<SurveyPageResult>(new SurveyPageResult()
            {
                Survey = survey
            });
        }
    }
}