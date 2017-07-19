using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.EfDAL
{
    public class EfRepository<TEnity> : IRepository<TEnity> where TEnity:class 
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;
        private readonly DbSet<TEnity> _set;

        public EfRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
            _set = _context.Set<TEnity>();
        }

        public TEnity Get(params object[] id)
        {
            return _set.Find(id);
        }

        public IEnumerable GetAll()
        {
            return _set.AsNoTracking().ToList();
        }

        public IEnumerable<TEnity> Get(Func<TEnity, bool> predicate)
        {
            return _set.AsNoTracking().Where(predicate).ToList();
        }

        public void Update(TEnity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TEnity item)
        {
            _set.Remove(item);
        }

        public void Delete(Func<TEnity, bool> predicate)
        {
            _set.RemoveRange(Get(predicate));
        }

        public void Add(TEnity item)
        {
            _set.Add(item);
        }
    }
}
