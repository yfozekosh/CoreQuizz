using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task<TEntity> GetAsync(params object[] id);
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        IQueryable<TEntity> GetAllQueryable();

        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);
        
        void Update(TEntity item);

        void Delete(TEntity item);
        
        Task DeleteAsync(Func<TEntity, bool> predicate);
        
        Task AddAsync(TEntity item);
    }
}