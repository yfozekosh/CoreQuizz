using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IRepository<TEntity> : IDisposable
    {
        TEntity Get(params object[] id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);

        void Update(TEntity item);

        void Delete(TEntity item);

        void Delete(Func<TEntity, bool> predicate);

        void Add(TEntity item);
    }
}