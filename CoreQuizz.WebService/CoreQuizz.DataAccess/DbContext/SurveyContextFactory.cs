using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CoreQuizz.DataAccess.DbContext
{
    public class SurveyContextFactory : IDbContextFactory<SurveyContext>
    {
        public SurveyContext Create(DbContextFactoryOptions options)
        {
            var connection = @"Server=tcp:corequizz-server.database.windows.net,1433;Initial Catalog=CoreQuizz;Persist Security Info=False;User ID=yfozekosh;Password=H25mc1bb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var builder = new DbContextOptionsBuilder<SurveyContext>();
            builder.UseSqlServer(connection);
            return new SurveyContext(builder.Options);
        }
    }
}