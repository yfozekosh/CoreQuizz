using System;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts
{
    public interface IRepository<T>
    {
        T Get<T2>(T2 id);
        void Update(T item);
        void Delete(T item);
        void Delete(Func<T, bool> predicate);
        void Add(T item);
        void Save();
    }
}