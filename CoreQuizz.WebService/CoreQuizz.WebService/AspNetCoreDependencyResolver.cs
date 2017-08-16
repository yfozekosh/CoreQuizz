using System;
using CoreQuizz.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.WebService
{
    public class AspNetCoreDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public AspNetCoreDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TRequest Resolve<TRequest>()
        {
            return _serviceProvider.GetService<TRequest>();
        }
    }
}
