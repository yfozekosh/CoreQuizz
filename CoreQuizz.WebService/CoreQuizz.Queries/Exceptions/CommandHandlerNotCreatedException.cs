using System;

namespace CoreQuizz.Queries.Exceptions
{
    public class CommandHandlerNotCreatedException : Exception
    {
        public CommandHandlerNotCreatedException(Type handlerType, Exception innerException)
            :base($"Handler {handlerType.Name} was not created.", innerException)
        {
        }
    }
}