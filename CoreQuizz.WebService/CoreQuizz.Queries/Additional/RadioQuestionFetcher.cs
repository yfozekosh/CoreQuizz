using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Additional.Abstract;
using CoreQuizz.Shared.DomainModel.Survey.Question;

namespace CoreQuizz.Queries.Additional
{
    public class RadioQuestionFetcher : QuestionFetcher<RadioQuestion>
    {
        public RadioQuestionFetcher(SurveyContext context) : base(context)
        {
        }

        public override async Task<RadioQuestion> FetchQuestionAsync(int id)
        {
            RadioQuestion radioQuestion = Context.Find<RadioQuestion>(id);

            if (radioQuestion != null)
            {
                await Context.Entry(radioQuestion).Collection(q => q.Options).LoadAsync();
            }
            
            return radioQuestion;
        }
    }
}