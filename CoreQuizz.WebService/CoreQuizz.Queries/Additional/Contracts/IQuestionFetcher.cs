using System.Threading.Tasks;
using CoreQuizz.Shared.DomainModel.Survey.Question;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Queries.Additional.Contracts
{
    public interface IQuestionFetcher
    {
        Task<Question> FetchQuestionAsync(int id);
    }
    
    public interface IQuestionFetcher<TQuestion> where TQuestion : Question
    {
        Task<TQuestion> FetchQuestionAsync(int id);
    }
}