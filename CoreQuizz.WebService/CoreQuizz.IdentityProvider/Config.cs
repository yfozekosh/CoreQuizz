using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;
using IdentityServer4.Models;

namespace CoreQuizz.IdentityProvider
{
    public static class Config
    {
        public static List<TestUser> GetUsers => new List<TestUser>
        {
            new TestUser()
            {
                SubjectId = "1",
                Username = "frank@email.com",
                Password = "passowrd",

                Claims = new List<Claim>()
                {
                    new Claim("role","admin"),
                    new Claim("email","frank@email.com"),
                    new Claim("email_verified","true")
                }
            },
            new TestUser()
            {
                SubjectId = "2",
                Username = "alice@email.com",
                Password = "password",

                Claims = new List<Claim>()
                {
                    new Claim("role","user"),
                    new Claim("email","alice@email.com"),
                    new Claim("email_verified","true")
                }
            },
        };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
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
