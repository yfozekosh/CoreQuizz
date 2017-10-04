using System.Threading.Tasks;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.Contracts
{
    public interface IQuestionDispatcher
    {
        Task<CommandResult> SaveAsync(Survey survey, Question question);

        Task<CommandResult> DeleteAsync(Survey survey, Question question);
    }

    public interface IQuestionDispatcher<in TQuestion> where TQuestion : Question
    {
        Task<CommandResult> SaveAsync(Survey survey, TQuestion question);

        Task<CommandResult> DeleteAsync(Survey survey, TQuestion question);
    }
}