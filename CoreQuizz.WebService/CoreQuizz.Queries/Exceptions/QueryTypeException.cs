using System;

namespace CoreQuizz.Queries.Exceptions
{
    public class QueryTypeException : Exception
    {
        public QueryTypeException()
        {
        }

        public QueryTypeException(string message) : base(message)
        {
        }

        public QueryTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}