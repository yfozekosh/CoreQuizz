using System;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException()
        {
        }

        public RepositoryNotFoundException(Type typeNotFound) : this($"Repository {typeNotFound.FullName} not found")
        {
        }

        public RepositoryNotFoundException(Type typeNotFound, Exception innerException)
            : this($"Repository {typeNotFound.FullName} not found", innerException)
        {
        }

        public RepositoryNotFoundException(string message) : base(message)
        {
        }

        public RepositoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}