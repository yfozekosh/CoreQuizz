using System.Threading.Tasks;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.Contracts
{
    public interface IQuestionSaver
    {
        Task<CommandResult> SaveAsync(Survey survey, Question question);
    }

    public interface IQuestionSaver<in TQuestion> where TQuestion : Question
    {
        Task<CommandResult> SaveAsync(Survey survey, TQuestion question);
    }
}