using Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<T> Get<T>() where T:BaseEntity;
        void Save();
        void Rollback();
    }
}