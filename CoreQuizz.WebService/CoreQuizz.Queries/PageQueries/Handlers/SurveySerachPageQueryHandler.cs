using System.Linq;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Queries.PageQueries.Handlers
{
    public class SurveySerachPageQueryHandler : IQueryHandler<SurveySearchPageQuery, SurveyListItem[]>
    {
        private readonly SurveyContext _context;

        public SurveySerachPageQueryHandler(SurveyContext context)
        {
            _context = context;
        }

        public SurveyListItem[] Execute(SurveySearchPageQuery query)
        {
            var surveyQuery = _context.Surveys
                .Include(s=>s.CreatedBy)
                .Where(s => s.Title.Contains(query.SearchText))
                .OrderBy(x => x.ModifieDateTime)
                .Skip(query.PageCount * query.PageNumber)
                .Take(query.PageCount);

            SurveyListItem[] result = surveyQuery.Select(survey => new SurveyListItem()
            {
                SurveyId = survey.Id,
                QuestionsCount = survey.Questions.Count,
                CreatedDate = survey.CreatedDate,
                ModifiedDate = survey.ModifieDateTime,
                Stars = 0,
                SurveyName = survey.Title,
                CreatedBy = survey.CreatedBy.Email
            }).ToArray();

            return result;
        }
    }
}