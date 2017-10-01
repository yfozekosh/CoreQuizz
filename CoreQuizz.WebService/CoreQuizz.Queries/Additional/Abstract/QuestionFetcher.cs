using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Additional.Contracts;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Queries.Additional.Abstract
{
    public abstract class QuestionFetcher<TQuestion> : IQuestionFetcher, IQuestionFetcher<TQuestion>
        where TQuestion : Question
    {
        protected readonly SurveyContext Context;

        protected QuestionFetcher(SurveyContext context)
        {
            Context = context;
        }

        async Task<Question> IQuestionFetcher.FetchQuestionAsync(int id)
        {
            return await FetchQuestionAsync(id);
        }

        public abstract Task<TQuestion> FetchQuestionAsync(int id);
    }
}