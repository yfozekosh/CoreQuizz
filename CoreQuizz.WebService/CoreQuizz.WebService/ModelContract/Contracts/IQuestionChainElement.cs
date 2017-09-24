using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.ModelContract
{
    internal interface IQuestionChainElement
    {
        QuestionChainResult Process(JObject jObject);
        IQuestionChainElement SetNext(IQuestionChainElement next);
    }
}