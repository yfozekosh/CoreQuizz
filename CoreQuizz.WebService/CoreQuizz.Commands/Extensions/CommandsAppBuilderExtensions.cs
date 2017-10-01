using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CoreQuizz.Commands.Additional.Contracts;
using CoreQuizz.Commands.Additional.QuestionSavers;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Commands.Exceptions;
using CoreQuizz.Commands.Handlers.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.Commands.Extensions
{
    public static class CommandsAppBuilderExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            
            RegisterCommands(services);

            services.AddTransient<IQuestionSaverFactory, QuestionSaverFactory>();

        }

        public static void RegisterCommands(IServiceCollection services)
        {
            var assembly = typeof(CommandsAppBuilderExtensions).GetTypeInfo().Assembly;
            IList<Type> commandTypes;
            IList<Type> handlerTypes;
            List<Tuple<Type, Type>> abstractionRealizationTuples = new List<Tuple<Type, Type>>();

            try
            {
                commandTypes = assembly.GetTypes().Where(type =>
                {
                    var isCommand = type.GetInterfaces().Contains(typeof(ICommand));
                    return isCommand && !type.GetTypeInfo().IsAbstract;
                }).ToList();

                handlerTypes = assembly.GetTypes().Where(type =>
                {
                    var isHandler = type.GetInterfaces().Where(i => i.GetTypeInfo().IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition()).Contains(typeof(ICommandHandler<>));
                    return isHandler && !type.GetTypeInfo().IsAbstract;
                }).ToList();
            }
            catch (Exception e)
            {
                throw new CommandTypeException("Cannot get command or handler types in assembly", e);
            }

            try
            {
                foreach (var commandType in commandTypes)
                {
                    Type abstractHandlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
                    List<Type> correspondingHandlerRealizations =
                        handlerTypes.Where(handler => handler.GetInterfaces().Contains(abstractHandlerType)).ToList();

                    if (correspondingHandlerRealizations.Count != 1)
                    {
                        throw new CommandTypeException(
                            $"No handlers for command {commandType.Name} or there is more then 1 realization");
                    }

                    Type realization = correspondingHandlerRealizations[0];
                    abstractionRealizationTuples.Add(new Tuple<Type, Type>(abstractHandlerType, realization));
                }
            }
            catch (Exception e)
            {
                throw new CommandTypeException("Cannot create handler-command tuple", e);
            }

            foreach (Tuple<Type, Type> typeTuple in abstractionRealizationTuples)
            {
                try
                {
                    services.AddTransient(typeTuple.Item1, typeTuple.Item2);
                }
                catch (Exception e)
                {
                    throw new CommandTypeNotRegisteredException(typeTuple.GetType(), e);
                }
            }
   
        }
    }
}