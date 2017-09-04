using System;
using System.Collections.Generic;

namespace CoreQuizz.BAL.Managers
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<Exception> Exceptions { get; set; }
        public T Result { get; set; }
    }
}