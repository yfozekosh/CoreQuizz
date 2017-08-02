using CoreQuizz.WebService.Enum;
using Microsoft.AspNetCore.Http;

namespace CoreQuizz.WebService.Session
{
    internal class SessionManager : ISessionManager
    {
        private readonly ISession _context;

        public SessionManager(ISession context)
        {
            _context = context;
        }

        public string CurrentLogin
        {
            get
            {
                return _context.GetString(SessionValues.AuthentificatedUser.ToString());
            }

            set
            {
                _context.SetString(SessionValues.AuthentificatedUser.ToString(), value);
            }
        }
    }
}