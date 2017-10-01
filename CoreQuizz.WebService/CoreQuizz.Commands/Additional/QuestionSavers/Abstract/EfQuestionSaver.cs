using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.QuestionSavers.Abstract
{
    public abstract class EfQuestionSaver<TQuestion> : QuestionSaver<TQuestion> where TQuestion : Question
    {
        protected readonly SurveyContext SurveyContext;

        protected EfQuestionSaver(SurveyContext surveyContext)
        {
            SurveyContext = surveyContext;
        }
    }
}