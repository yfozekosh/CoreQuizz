using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CoreQuizz.WebService
{
    public static class Authentication
    {
        public static void UseIdentityServer(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = configuration["Authority"],
                RequireHttpsMetadata = true,

                ClientId = "corequizz_mvc",
                SaveTokens = true,
                Scope = { "role"}
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}", defaults: new
                {
                    controller = "Home",
                    action = "index"
                });
            });
        }
    }
}
