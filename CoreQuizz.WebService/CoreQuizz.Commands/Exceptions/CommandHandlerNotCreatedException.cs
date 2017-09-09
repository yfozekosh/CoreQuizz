using System;

namespace CoreQuizz.Commands.Exceptions
{
    public class CommandHandlerNotCreatedException : Exception
    {
        public CommandHandlerNotCreatedException(Type handlerType, Exception innerException)
            :base($"Handler {handlerType.Name} was not created.", innerException)
        {
        }

        public CommandHandlerNotCreatedException(Type handlerType) : this(handlerType, null)
        {
        }
    }
}