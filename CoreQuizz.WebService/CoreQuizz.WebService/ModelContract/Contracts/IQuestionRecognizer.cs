using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.ModelContract.Contracts
{
    public interface IQuestionRecognizer
    {
        Question Recognize(JObject jObject);
    }
}