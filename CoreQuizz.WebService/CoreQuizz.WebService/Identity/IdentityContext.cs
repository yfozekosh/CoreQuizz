using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.WebService.Identity
{
    public class IdentityContext : IdentityDbContext<AuthenticationUser>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}