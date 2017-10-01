using System;

namespace CoreQuizz.Queries.Additional.Contracts
{
    public interface IQuestionFetcherFactory
    {
        IQuestionFetcher GetFetcher(Type questionType);
    }
}