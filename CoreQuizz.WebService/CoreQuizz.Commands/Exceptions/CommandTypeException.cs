using System;
using System.Runtime;

namespace CoreQuizz.Commands.Exceptions
{
    public class CommandTypeException : Exception
    {
        public CommandTypeException()
        {
        }

        public CommandTypeException(string message) : base(message)
        {
        }

        public CommandTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}