using System.Collections.Generic;
using System.Linq;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Queries.PageQueries
{
    public class SurveyListPageQueryHandler : IQueryHandler<SurveyListPageQuery, SurveyListItem[]>
    {
        private readonly IRepository<Survey> _surveyRepository;
        private IRepository<Question> _questionRepository;

        public SurveyListPageQueryHandler(IUnitOfWork unitOfWork)
        {
            _surveyRepository = unitOfWork.GetRepository<Survey>();
            _questionRepository = unitOfWork.GetRepository<Question>();
        }

        public SurveyListItem[] Execute(SurveyListPageQuery query)
        {
            var surveys = _surveyRepository.GetAllQueryable().Include(x=>x.CreatedBy).Where(x=>x.CreatedBy.Id == query.UserId);
            
            var result = surveys.ToArray().Select(survey => new
            {
                Id = survey.Id,
                CreatedDate = survey.CreatedDate,
                ModifiedDate = survey.ModifieDateTime,
                SurveyName = survey.Title,
                Count = _questionRepository.GetAllQueryable().Count(x => x.Survey.Id==survey.Id),
                Stars = 0
            }).ToArray();
            
            return result.Select(res=>new SurveyListItem()
            {
                CreatedDate = res.CreatedDate,
                ModifiedDate = res.ModifiedDate,
                SurveyName = res.SurveyName,
                QuestionsCount = res.Count,
                Stars = 0
            }).ToArray();

        }
    }
}