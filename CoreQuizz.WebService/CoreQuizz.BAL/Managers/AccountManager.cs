using System;
using System.Linq;
using System.Security.Cryptography;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using CoreQuizz.BAL.Contracts;

namespace CoreQuizz.BAL
{
    public class AccountManager : IAccountManager, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void RegisterUser(string login, string password)
        {
            IRepository<User> userRepo = _unitOfWork.GetRepository<User>();
            Tuple<string, byte[]> hashSaltTuple = HashString(password);

            User user = new User
            {
                Email = login,
                Salt = Convert.ToBase64String(hashSaltTuple.Item2),
                PasswordHash = hashSaltTuple.Item1,
                CreatedDate = DateTime.Now,
                ModifieDateTime = DateTime.Now
            };

            userRepo.Add(user);
            _unitOfWork.Save();
        }

        public bool IsUserExists(string login)
        {
            IRepository<User> userRepo = _unitOfWork.GetRepository<User>();
            return userRepo.Get(user=>user.Email==login).Any();
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

        public bool LogInUser(string login, string password)
        {
            IRepository<User> userRepo = _unitOfWork.GetRepository<User>();
            User user = userRepo.Get(x => x.Email == login).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException($"User {login} do not exists");
            }

            byte[] salt = Convert.FromBase64String(user.Salt);
            string hashedPassword = HashString(password, salt).Item1;

            return hashedPassword == user.PasswordHash;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
