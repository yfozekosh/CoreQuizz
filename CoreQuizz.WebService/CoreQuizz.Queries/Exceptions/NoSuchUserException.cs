using System;

namespace CoreQuizz.Queries.Exceptions
{
    public class NoSuchUserException : Exception
    {
        public NoSuchUserException(int id) : base($"No user with id {id}")
        {   
        }
    }
}