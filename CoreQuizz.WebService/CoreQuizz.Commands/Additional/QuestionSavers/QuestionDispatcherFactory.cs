using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreQuizz.Commands.Additional.Contracts;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.QuestionSavers
{
    public class QuestionDispatcherFactory : IQuestionDispatcherFactory
    {
        private readonly SurveyContext _context;

        public QuestionDispatcherFactory(SurveyContext context)
        {
            _context = context;
        }
        
        public IQuestionDispatcher GetDispatcher(Question question)
        {
            Assembly assembly = typeof(QuestionDispatcherFactory).GetTypeInfo().Assembly;
            IEnumerable<Type> saverTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IQuestionDispatcher)))
                .Where(t => !t.GetTypeInfo().IsAbstract);

            Dictionary<Type, Type> questionTypeDisctionary =
                saverTypes.ToDictionary(t =>
                {
                    var interfaces = t.GetInterfaces();
                    var first = interfaces.First(i => i.IsConstructedGenericType);
                    var genericArguments = first.GetGenericArguments();
                    return genericArguments.First();
                }, t => t);

            Type result = questionTypeDisctionary.FirstOrDefault(t => t.Key.IsInstanceOfType(question)).Value;
            
            // TODO: check for constructor parameters
            return (IQuestionDispatcher) Activator.CreateInstance(result, _context);
        }
    }
}