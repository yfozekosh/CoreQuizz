using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.Contracts
{
    public interface IQuestionSaverFactory
    {
        IQuestionSaver GetSaver(Question question);
         }
}