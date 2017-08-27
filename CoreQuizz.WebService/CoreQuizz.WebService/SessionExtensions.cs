using System;
using CoreQuizz.WebService.Session;
using Microsoft.Extensions.DependencyInjection;

namespace CoreQuizz.WebService
{
    public static class SessionExtensions
    {
        public static void AddSession(this IServiceCollection services)
        {
            services.AddTransient<ISessionManagerFactory, SessionManagerFactory>();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.CookieHttpOnly = true;
                options.CookieSecure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
            });
        }
    }
}