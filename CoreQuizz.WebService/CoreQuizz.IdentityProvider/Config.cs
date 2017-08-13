using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
                    new Claim("role","admin")
                }
            },
            new TestUser()
            {
                SubjectId = "2",
                Username = "alice@email.com",
                Password = "password",

                Claims = new List<Claim>()
                {
                    new Claim("role","admin")
                }
            },
        };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResource()
                {
                    Name="corequizzapi",
                    DisplayName="Core Quizz Api",
                    UserClaims= {"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientName = "Quizz",
                    ClientId = "corequizzweb",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "corequizzapi"
                    }
                }
            };
        }
    }
}
