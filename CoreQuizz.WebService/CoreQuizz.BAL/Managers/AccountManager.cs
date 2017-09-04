using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CoreQuizz.BAL.Managers
{
    public class AccountManager : IAccountManager, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<User>> RegisterUserAsync(string login)
        {
            try
            {
                IRepository<User> userRepo = _unitOfWork.GetRepository<User>();

                User user = new User
                {
                    Email = login,
                    CreatedDate = DateTime.Now,
                    ModifieDateTime = DateTime.Now
                };

                await userRepo.AddAsync(user);
                await _unitOfWork.SaveAsync();

                return new OperationResult<User>()
                {
                    IsSuccess = true,
                    Result = user
                };
            }
            catch (Exception e)
            {
                return new OperationResult<User>
                {
                    IsSuccess = false,
                    Exceptions = new[] { e }
                };
            }
        }

        public async Task<bool> IsUserExistsAsync(string login)
        {
            IRepository<User> userRepo = _unitOfWork.GetRepository<User>();
            return (await userRepo.GetAsync(user => user.Email == login)).Any();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            IRepository<User> userRepo = _unitOfWork.GetRepository<User>();
            return userRepo.GetAsync(id);
        }
    }
}
