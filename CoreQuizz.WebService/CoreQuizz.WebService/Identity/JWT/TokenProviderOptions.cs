using System;
using Microsoft.IdentityModel.Tokens;

namespace CoreQuizz.WebService.Identity.JWT
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(120);

        public SigningCredentials SigningCredentials { get; set; }
    }
}