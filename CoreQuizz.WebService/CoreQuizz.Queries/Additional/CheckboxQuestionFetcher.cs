using System.Threading.Tasks;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Additional.Abstract;
using CoreQuizz.Shared.DomainModel.Survey.Question;

namespace CoreQuizz.Queries.Additional
{
    public class CheckboxQuestionFetcher : QuestionFetcher<CheckboxQuestion>
    {
        public CheckboxQuestionFetcher(SurveyContext context) : base(context)
        {
        }

        public override async Task<CheckboxQuestion> FetchQuestionAsync(int id)
        {
            CheckboxQuestion checkboxQuestion = Context.Find<CheckboxQuestion>(id);

            if (checkboxQuestion != null)
            {
                await Context.Entry(checkboxQuestion).Collection(q => q.Options).LoadAsync();
            }
            
            return checkboxQuestion;
        }
    }
}