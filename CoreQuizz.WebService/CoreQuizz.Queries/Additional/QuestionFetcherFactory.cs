using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Queries.Additional.Contracts;

namespace CoreQuizz.Queries.Additional
{
    public class QuestionFetcherFactory : IQuestionFetcherFactory
    {
        private readonly SurveyContext _context;

        public QuestionFetcherFactory(SurveyContext context)
        {
            _context = context;
        }

        public IQuestionFetcher GetFetcher(Type questionType)
        {
            Assembly assembly = typeof(QuestionFetcherFactory).GetTypeInfo().Assembly;
            IEnumerable<Type> saverTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IQuestionFetcher)))
                .Where(t => !t.GetTypeInfo().IsAbstract);

            Dictionary<Type, Type> questionTypeDisctionary =
                saverTypes.ToDictionary(t =>
                {
                    var interfaces = t.GetInterfaces();
                    var first = interfaces.First(i => i.IsConstructedGenericType);
                    var genericArguments = first.GetGenericArguments();
                    return genericArguments.First();
                }, t => t);

            Type fetcherType = questionTypeDisctionary.First(kv => questionType.IsAssignableFrom(kv.Key)).Value;
            
            // TODO: check parameters

            return (IQuestionFetcher) Activator.CreateInstance(fetcherType, _context);
        }
    }
}