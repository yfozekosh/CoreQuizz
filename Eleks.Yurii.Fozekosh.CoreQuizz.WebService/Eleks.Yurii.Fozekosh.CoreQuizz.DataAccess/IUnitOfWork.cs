namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<T> Get<T>();
        void Save();
        void Rollback();
    }
}