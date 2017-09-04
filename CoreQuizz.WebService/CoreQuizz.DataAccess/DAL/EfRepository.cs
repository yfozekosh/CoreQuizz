using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.Contract.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;

namespace CoreQuizz.DataAccess.DAL
{
    public class EfRepository<TEnity> : IRepository<TEnity> where TEnity : class
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;
        private readonly DbSet<TEnity> _set;

        public EfRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
            _set = _context.Set<TEnity>();
        }


        public void Dispose()
        {
            
        }


        public Task<TEnity> GetAsync(params object[] id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return _set.FindAsync(id);
        }

        public async Task<IEnumerable<TEnity>> GetAllAsync()
        {
            return await _set.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEnity> GetAllQueryable()
        {
            return _set.AsNoTracking();
        }

        public async Task<IEnumerable<TEnity>> GetAsync(Func<TEnity, bool> predicate, params Expression<Func<TEnity, object>>[] includes)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            IQueryable<TEnity> query = _set.AsNoTracking();

            if (includes.Length != 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.Where(predicate).ToAsyncEnumerable().ToList();
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

        public async Task DeleteAsync(Func<TEnity, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            _set.RemoveRange(await GetAsync(predicate));
        }

        public Task AddAsync(TEnity item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return _set.AddAsync(item);
        }
    }
}
