using System;
using CoreQuizz.BAL.Exceptions;
using CoreQuizz.BAL.Contracts;

namespace CoreQuizz.BAL
{
    internal abstract class ChainElement<T1, T2> : IChainElement<T1, T2>
    {
        private readonly IChainElement<T1, T2> _nextElement;

        protected ChainElement(IChainElement<T1,T2> nextElement = null)
        {
            _nextElement = nextElement;
        }

        protected abstract ChainResult<T2> _Run(T1 args);

        public T2 Run(T1 args)
        {
            ChainResult<T2> currentResult = _Run(args);
            if (currentResult.IsResultFound)
            {
                return currentResult.Result;
            }

            if (_nextElement == null) throw new NoResultFoundException();

            return _nextElement.Run(args);
        }

        object IChainElement.Run(object args)
        {
            if (!(args is T1))
            {
                throw new ArgumentException($"Type of args should be {typeof(T1)}, got {args.GetType()}");
            }

            return Run((T1) args);
        }
    }
}