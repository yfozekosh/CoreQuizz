using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.Contracts
{
    public interface IQuestionDispatcherFactory
    {
        IQuestionDispatcher GetDispatcher(Question question);
         }
}