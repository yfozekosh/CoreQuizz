using System;

namespace CoreQuizz.BAL.Exceptions
{
    public class NoResultFoundException : Exception
    {
        public NoResultFoundException() : base("No result was found in chain")
        {
        }
    }
}