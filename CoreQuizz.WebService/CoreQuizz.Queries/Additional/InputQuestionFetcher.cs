using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Additional.Abstract;
using CoreQuizz.Shared.DomainModel.Survey.Question;

namespace CoreQuizz.Queries.Additional
{
    public class InputQuestionFetcher : QuestionFetcher<InputQuestion>
    {
        public InputQuestionFetcher(SurveyContext context) : base(context)
        {
        }

        public override Task<InputQuestion> FetchQuestionAsync(int id)
        {
            InputQuestion question = Context.Find<InputQuestion>(id);

            return Task.FromResult(question);
        }
    }
}