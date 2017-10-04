using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.QuestionSavers.Abstract
{
    public abstract class EfQuestionDispatcher<TQuestion> : QuestionDispatcher<TQuestion> where TQuestion : Question
    {
        protected readonly SurveyContext SurveyContext;

        protected EfQuestionDispatcher(SurveyContext surveyContext)
        {
            SurveyContext = surveyContext;
        }
    }
}