using Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.DbContext
{
    public class SurveyContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);
                entity.HasKey(x => x.Email);
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
