namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<T> Get<T>() where T:class ;
        void Save();
        void Rollback();
    }
}