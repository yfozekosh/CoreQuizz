using System;

namespace CoreQuizz.DataAccess.Exceptions
{
    public class RepositoryNotCreatedException : Exception
    {
        public RepositoryNotCreatedException()
        {
        }

        public RepositoryNotCreatedException(Type typeNotFound) : this($"Repository {typeNotFound.FullName} not found")
        {
        }

        public RepositoryNotCreatedException(Type typeNotFound, Exception innerException)
            : this($"Repository {typeNotFound.FullName} not found", innerException)
        {
        }

        public RepositoryNotCreatedException(string message) : base(message)
        {
        }

        public RepositoryNotCreatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}