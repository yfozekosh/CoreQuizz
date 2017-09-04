using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreQuizz.DataAccess.Contract;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.DataAccess.Exceptions;
using Microsoft.Extensions.Logging;

namespace CoreQuizz.DataAccess.DAL
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly SurveyContext _context;
        private readonly ILogger<EfUnitOfWork> _logger;

        public EfUnitOfWork(SurveyContext surveyContext, ILogger<EfUnitOfWork> logger)
        {
            _context = surveyContext;
            this._logger = logger;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            try
            {
                Type efRepoType = typeof(EfRepository<>);
                ConstructorInfo[] constructors = efRepoType.GetConstructors();

                bool isAnyConstructorSuitable = constructors.Any(constructor =>
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    if (parameters.Length != 1)
                    {
                        return false;
                    }
                    bool isParameterDbContext = parameters[0].ParameterType.IsInstanceOfType(_context);
                    return isParameterDbContext;
                });

                if (!isAnyConstructorSuitable)
                {
                    throw new NoSuitableConstructorException();
                }

                return (IRepository<T>)Activator.CreateInstance(typeof(EfRepository<T>), _context);
            }
            catch (Exception e)
            {
                throw new RepositoryNotCreatedException(typeof(T), e);
            }
        }

        public async Task<UnitOfWorkActionResult> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return new UnitOfWorkActionResult()
                {
                    Exception = null,
                    IsSuccessfull = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return new UnitOfWorkActionResult()
                {
                    Exception = e,
                    IsSuccessfull = false
                };
            }
        }

        public async Task<UnitOfWorkActionResult> RollbackAsync()
        {
            try
            {
                (await _context
                        .ChangeTracker
                        .Entries()
                        .ToAsyncEnumerable()
                        .ToList())
                    .ForEach(x => x.Reload());

                return new UnitOfWorkActionResult()
                {
                    Exception = null,
                    IsSuccessfull = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return new UnitOfWorkActionResult()
                {
                    Exception = e,
                    IsSuccessfull = false
                };
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
