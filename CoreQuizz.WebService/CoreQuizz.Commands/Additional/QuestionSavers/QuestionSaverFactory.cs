using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreQuizz.Commands.Additional.Contracts;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Commands.Additional.QuestionSavers
{
    public class QuestionSaverFactory : IQuestionSaverFactory
    {
        private readonly SurveyContext _context;

        public QuestionSaverFactory(SurveyContext context)
        {
            _context = context;
        }
        
        public IQuestionSaver GetSaver(Question question)
        {
            Assembly assembly = typeof(QuestionSaverFactory).GetTypeInfo().Assembly;
            IEnumerable<Type> saverTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IQuestionSaver)))
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
            return (IQuestionSaver) Activator.CreateInstance(result, _context);
        }
    }
}