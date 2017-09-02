using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CoreQuizz.WebService.Identity
{
    public class AuthOptions
    {
        public const string Issuer = "CoreQuizzServer"; // издатель токена
        public const string Audience = "http://localhost:4200/"; // потребитель токена
        const string Key = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int Lifetime = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
