using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.WebService.ModelContract
{
    internal class QuestionChainResult
    {
        public Question Result { get; set; }
        public bool IsFound { get; set; }
    }
}