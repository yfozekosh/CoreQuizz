using System;

namespace CoreQuizz.DataAccess.Exceptions
{
    public class NoSuitableConstructorException : Exception
    {
        public NoSuitableConstructorException()
        {
        }

        public NoSuitableConstructorException(string message) : base(message)
        {
        }

        public NoSuitableConstructorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}