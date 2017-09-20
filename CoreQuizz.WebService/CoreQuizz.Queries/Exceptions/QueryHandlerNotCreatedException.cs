using System;

namespace CoreQuizz.Queries.Exceptions
{
    public class QueryHandlerNotCreatedException : Exception
    {
        public QueryHandlerNotCreatedException(Type handlerType, Exception innerException)
            :base($"Handler {handlerType.Name} was not created.", innerException)
        {
        }

        public QueryHandlerNotCreatedException(Type handlerType) : this(handlerType, null)
        {
        }
    }
}