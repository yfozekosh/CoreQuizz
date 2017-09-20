using System;

namespace CoreQuizz.Queries.Exceptions
{
    public class QueryTypeNotRegisteredException : Exception
    {
        public QueryTypeNotRegisteredException(Type type)
            :this(type, null)
        {
            
        }

        public QueryTypeNotRegisteredException(Type type, Exception innerException)
            : base($"Command type {type.Name} was not registered", innerException)
        {
            
        }
    }
}