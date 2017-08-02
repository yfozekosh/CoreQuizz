using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Contracts
{
    public interface IQuestionManager
    {
        Question LoadFullQuestion(Question question);

        IList<Question> LoadFullQuestions(IList<Question> questions);

        bool IsQuestionsFull(IList<Question> questions);
    }
}
