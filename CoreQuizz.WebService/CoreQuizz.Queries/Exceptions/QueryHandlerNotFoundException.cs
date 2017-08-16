using System;

namespace CoreQuizz.Queries.PageQueries
{
    public class QueryHandlerNotFoundException : Exception
    {
        public QueryHandlerNotFoundException()
        {
        }

        public QueryHandlerNotFoundException(Type type) : this($"Query handler for {type} not found")
        {
            
        }

        public QueryHandlerNotFoundException(string message) : base(message)
        {
        }

        public QueryHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}