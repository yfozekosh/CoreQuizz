using Microsoft.AspNetCore.Http;

namespace CoreQuizz.WebService.Session
{
    public interface ISessionManagerFactory
    {
        ISessionManager GetSessionManager(ISession session);
    }
}