using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoreQuizz.WebService.Identity
{
    public class AuthenticationUser : IdentityUser
    {
        public string AccountPurpose { get; set; }

        public int CoreQuizzUserId { get; set; }
    }
}