using System;
using System.Collections.Generic;
using System.Text;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess
{
    public class EfRepository<T> : IRepository<T>
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _context;

        public EfRepository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            _context = context;
        }

        public T Get<T2>(T2 id)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
