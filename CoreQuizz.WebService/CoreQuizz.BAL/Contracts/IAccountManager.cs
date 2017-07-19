namespace CoreQuizz.BAL.Contracts
{
    public interface IAccountManager
    {
        void RegisterUser(string login, string password);

        bool IsUserExists(string login);

        bool LogInUser(string login, string password);
    }
}