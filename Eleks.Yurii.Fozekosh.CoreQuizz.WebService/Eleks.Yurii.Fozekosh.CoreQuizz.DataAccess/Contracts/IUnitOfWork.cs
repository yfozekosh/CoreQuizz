namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T:class ;
        void Save();
        void Rollback();
    }
}