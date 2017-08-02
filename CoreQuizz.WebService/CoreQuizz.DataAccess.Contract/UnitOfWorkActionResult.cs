using System;

namespace CoreQuizz.DataAccess.Contract
{
    public class UnitOfWorkActionResult
    {
        public virtual bool IsSuccessfull { get; set; }
        public virtual Exception Exception { get; set; }
    }
}