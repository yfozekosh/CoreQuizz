using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IRepository<TEntity> : IDisposable
    {
        TEntity Get(params object[] id);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetAllQueryable();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);

        void Update(TEntity item);

        void Delete(TEntity item);

        void Delete(Func<TEntity, bool> predicate);

        void Add(TEntity item);
    }
}