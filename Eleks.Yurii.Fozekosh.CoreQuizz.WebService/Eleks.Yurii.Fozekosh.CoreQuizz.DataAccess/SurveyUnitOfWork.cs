using System;
using System.Collections.Generic;
using System.Text;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.DbContext;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess
{
    class SurveyUnitOfWork : IUnitOfWork
    {
        private IDictionary<Type, IRepository<object>> _repositories;
        private readonly SurveyContext _context;

        public SurveyUnitOfWork(SurveyContext surveyContext)
        {
            _context = surveyContext;
            _repositories = new Dictionary<Type, IRepository<object>>();
        }

        IRepository<T> IUnitOfWork.Get<T>()
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repo = new EfRepository<T>(_context);
                _repositories[typeof(T)]=repo as IRepository<object>;
            }

            return _repositories[typeof(T)] as IRepository<T>;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
