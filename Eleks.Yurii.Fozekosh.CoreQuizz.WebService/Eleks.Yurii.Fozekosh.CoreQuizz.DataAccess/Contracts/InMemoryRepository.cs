using System;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        
        
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