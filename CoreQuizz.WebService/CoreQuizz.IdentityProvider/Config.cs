using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;
using IdentityServer4.Models;

namespace CoreQuizz.IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "corequizz_web",
                    ClientName = "Core Qizz JS Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =           { "http://localhost:4200/auth.html" },
                    PostLogoutRedirectUris = { "http://localhost:4200/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        "corequizzapi"
                    }
                },
                new Client()
                {
                    ClientId = "corequizz_mvc",
                    ClientName = "Core Quizz MVC",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,

                    RedirectUris = { "https://localhost:44304/" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        "corequizzapi"
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("corequizzapi","Core Quizz Api")
                {
                    UserClaims =
                    {
                        "role"
                    }
                }
            };
        }
    }
}