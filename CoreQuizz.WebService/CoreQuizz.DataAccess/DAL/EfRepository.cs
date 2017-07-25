using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreQuizz.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.DataAccess.DAL
{
    public class EfRepository<TEnity> : IRepository<TEnity> where TEnity : class
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;
        private readonly DbSet<TEnity> _set;

        public EfRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _context = context;
            _set = _context.Set<TEnity>();
        }

        public TEnity Get(params object[] id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return _set.Find(id);
        }

        public IEnumerable GetAll()
        {
            return _set.AsNoTracking().ToList();
        }

        public void Update(TEnity item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TEnity item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _set.Remove(item);
        }

        public void Delete(Func<TEnity, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            _set.RemoveRange(Get(predicate));
        }

        public void Add(TEnity item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _set.Add(item);
        }

        public IEnumerable<TEnity> Get(Func<TEnity, bool> predicate, params Expression<Func<TEnity, object>>[] includes)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            IQueryable<TEnity> query = _set.AsNoTracking();

            if (includes.Length != 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.Where(predicate).ToList();
        }
    }
}
