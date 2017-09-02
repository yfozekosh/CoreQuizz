using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreQuizz.DataAccess.DbContext
{
    public class SurveyContextFactory : IDbContextFactory<SurveyContext>
    {
        public SurveyContext Create(DbContextFactoryOptions options)
        {
            var connection = @"Server=(localdb)\corequizz;Database=CoreQuizz.SurveyDB;Trusted_Connection=True;";
            var builder = new DbContextOptionsBuilder<SurveyContext>();
            builder.UseSqlServer(connection);
            return new SurveyContext(builder.Options);
        }
    }
}