using System;
using System.Collections.Generic;

namespace CoreQuizz.Queries.Contract.Result
{
    public class QueryResult<TResult>
    {
        public QueryResult(bool isSucccess)
        {
            IsSuccess = isSucccess;
        }

        /// <summary>
        /// Is query was succefully executed.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Result of query execution.
        /// </summary>
        public TResult Value { get; set; }

        /// <summary>
        /// Enumerable of exception occured durring command execution. For internal use only.
        /// </summary>
        public IEnumerable<Exception> Exceptions { get; set; }

        /// <summary>
        /// Error occured durring command execution. Can be shown to user.
        /// </summary>
        public string Error { get; set; }
    }
}