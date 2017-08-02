using Microsoft.AspNetCore.Http;

namespace CoreQuizz.WebService.Session
{
    internal class SessionManagerFactory : ISessionManagerFactory
    {
        public ISessionManager GetSessionManager(ISession session)
        {
            return new SessionManager(session);
        }
    }
}