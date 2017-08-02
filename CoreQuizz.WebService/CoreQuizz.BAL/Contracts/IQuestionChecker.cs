using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Contracts
{
    public interface IQuestionChecker
    {
        bool IsQuestionsFilled(IList<Question> questions);
    }
}