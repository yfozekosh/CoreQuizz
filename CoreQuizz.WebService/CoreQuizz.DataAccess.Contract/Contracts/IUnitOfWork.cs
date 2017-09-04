using System;
using System.Threading.Tasks;

namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T:class ;
        Task<UnitOfWorkActionResult> SaveAsync();
        Task<UnitOfWorkActionResult> RollbackAsync();
    }
}