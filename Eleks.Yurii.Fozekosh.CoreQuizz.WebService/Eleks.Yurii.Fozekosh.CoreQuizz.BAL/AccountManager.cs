using System;
using System.Security.Cryptography;
using System.Text;
using Eleks.Yurii.Fozekosh.CoreQuizz.BAL.Contracts;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts;
using Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.BAL
{
    public class AccountManager : IAccountManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void RegisterUser(string login, string password)
        {
            IRepository<User> userRepo = _unitOfWork.Get<User>();
            Tuple<string, byte[]> hashSaltTuple = HashString(password);

            User user = new User()
            {
                Email = login,
                Salt = Encoding.ASCII.GetString(hashSaltTuple.Item2),
                PasswordHash = hashSaltTuple.Item1,
                CreatedDate = DateTime.Now,
                ModifieDateTime = DateTime.Now
            };

            userRepo.Add(user);
            _unitOfWork.Save();
        }

        public bool IsUserExists(string login)
        {
            IRepository<User> userRepo = _unitOfWork.Get<User>();
            return userRepo.Get(login) == null;
        }

        private Tuple<string,byte[]> HashString(string str, byte[] salt = null)
        {
            if (salt == null)
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(str, salt, KeyDerivationPrf.HMACSHA512, 10000, 32));
            return new Tuple<string,byte[]>(hash,salt);
        }
    }
}
