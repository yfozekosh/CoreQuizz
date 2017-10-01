using System;
using System.Collections.Generic;

namespace CoreQuizz.Commands.Contract
{
    public class CommandResult
    {
        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Is command was succefully executed.
        /// </summary>
        public bool IsSuccess { get; set; }

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