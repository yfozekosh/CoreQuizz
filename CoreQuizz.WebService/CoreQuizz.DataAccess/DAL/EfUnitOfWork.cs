using System;
using System.Linq;
using CoreQuizz.DataAccess.Contracts;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.DataAccess.Exceptions;

namespace CoreQuizz.DataAccess.DAL
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SurveyContext _context;

        public EfUnitOfWork(SurveyContext surveyContext)
        {
            _context = surveyContext;
        }

        public IRepository<T> GetRepository<T>() where T:class
        {
            try
            {
                //TODO: EfRepository Constructor check parameters count
                return (IRepository<T>)Activator.CreateInstance(typeof(EfRepository<T>), _context);
            }
            catch (Exception e)
            {
                throw new RepositoryNotCreatedException(typeof(T), e);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context
            .ChangeTracker
            .Entries()
            .ToList()
            .ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
