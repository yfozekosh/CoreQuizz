using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Contracts
{
    public interface IQuestionFiller
    {
        IList<Question> Fill(IList<Question> questions);
    }
}