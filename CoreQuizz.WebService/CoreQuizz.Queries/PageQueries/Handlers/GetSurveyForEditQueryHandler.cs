using System;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Additional.Contracts;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.PageQueries.Handlers.Abstract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.Shared.DomainModel.Survey;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class GetSurveyForEditQueryHandler : EfQueryHandler<GetSurveyForEditQuery, SurveyDefinitionViewModel>
    {
        private readonly IQuestionFetcherFactory _fetcherFactory;

        public GetSurveyForEditQueryHandler(SurveyContext context, IQuestionFetcherFactory fetcherFactory) :
            base(context)
        {
            _fetcherFactory = fetcherFactory;
        }

        public override async Task<QueryResult<SurveyDefinitionViewModel>> ExecuteAsync(GetSurveyForEditQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            User user = await Context.Users.FindAsync(query.UserId);
            if (user == null)
            {
                return new ErrorQueryResult<SurveyDefinitionViewModel>("No such user in db");
            }

            int id = query.SurveyId;

            Survey survey = await Context.Surveys
                .Include(s => s.Questions)
                .Include(s => s.Stars)
                .SingleOrDefaultAsync(s => s.Id == query.SurveyId && s.CreatedBy.Id == query.UserId);

            if (survey == null)
            {
                return new ErrorQueryResult<SurveyDefinitionViewModel>($"No survey with id {id} in database.");
            }

            SurveyDefinitionViewModel result = new SurveyDefinitionViewModel()
            {
                SurveyName = survey.Title,
                SurveyId = survey.Id,
                CreatedBy = survey.CreatedBy.Email,
                CreatedDate = survey.CreatedDate,
                Description = survey.Description,
                ModifiedDate = survey.ModifieDateTime,
                Stars = survey.Stars.Count,
                Questions = await Task.WhenAll(survey.Questions.Select(q =>
                    _fetcherFactory.GetFetcher(q.GetType()).FetchQuestionAsync(q.Id)))
            };
            return new OkQueryResult<SurveyDefinitionViewModel>(result);
        }
    }
}