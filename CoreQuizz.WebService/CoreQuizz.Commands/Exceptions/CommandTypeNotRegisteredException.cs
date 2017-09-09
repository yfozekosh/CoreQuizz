using System;

namespace CoreQuizz.Commands.Exceptions
{
    public class CommandTypeNotRegisteredException : Exception
    {
        public CommandTypeNotRegisteredException(Type type)
            :this(type, null)
        {
            
        }

        public CommandTypeNotRegisteredException(Type type, Exception innerException)
            : base($"Command type {type.Name} was not registered", innerException)
        {
            
        }
    }
}