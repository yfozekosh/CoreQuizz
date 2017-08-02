using System;

namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T:class ;
        UnitOfWorkActionResult Save();
        UnitOfWorkActionResult Rollback();
    }
}