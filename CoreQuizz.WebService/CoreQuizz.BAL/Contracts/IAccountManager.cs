using System.Threading.Tasks;
using CoreQuizz.BAL.Managers;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Contracts
{
    public interface IAccountManager
    {
        Task<OperationResult<User>>  RegisterUserAsync(string login);

        Task<bool> IsUserExistsAsync(string login);

        Task<User> GetUserByIdAsync(int id);
    }
}