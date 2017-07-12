using System;
using System.Collections.Generic;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess
{
    public interface IDataSaver<T>
    {
        void Save(IList<T> data);
        IList<T> Read();
    }

    class DataSaver<T> : IDataSaver<T>
    {
        public void Save(IList<T> data)
        {
            
            throw new NotImplementedException();
        }

        public IList<T> Read()
        {
            throw new NotImplementedException();
        }
    }

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