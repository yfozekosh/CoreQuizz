namespace Eleks.Yurii.Fozekosh.CoreQuizz.BAL.Contracts
{
    public interface IAccountManager
    {
        void RegisterUser(string login, string password);

        bool IsUserExists(string login);
    }
}