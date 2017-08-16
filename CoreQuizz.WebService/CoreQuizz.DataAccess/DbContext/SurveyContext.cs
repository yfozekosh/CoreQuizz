using CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.DataAccess.DbContext
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
                entity.HasIndex(user => user.Email).IsUnique();
                entity.HasMany(x => x.Surveys).WithOne(survey => survey.CreatedBy);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(survey => survey.Id);
                entity.HasMany(survey => survey.Questions).WithOne(question => question.Survey);
            });

            modelBuilder.Entity<Question>().HasKey(question => question.Id);
            modelBuilder.Entity<Question>().HasDiscriminator<string>("Type");

            modelBuilder.Entity<RadioQuestion>().HasBaseType<Question>();

            modelBuilder.Entity<RadioQuestion>(entity =>
            {
                entity.HasBaseType<Question>();
                entity.HasMany(question => question.Options).WithOne(option => (RadioQuestion)option.Question);
                
                entity.HasDiscriminator<string>("Type").HasValue(QuestionType.Radio.ToString());
            });

            modelBuilder.Entity<CheckboxQuestion>(entity =>
            {
                entity.HasBaseType<Question>();
                entity.HasMany(question => question.Options).WithOne(option => (CheckboxQuestion) option.Question);
                entity.HasDiscriminator<string>("Type").HasValue(QuestionType.Checkbox.ToString());
            });

            modelBuilder.Entity<InputQuestion>(entity =>
            {
                entity.HasBaseType<Question>();
                entity.HasDiscriminator<string>("Type").HasValue(QuestionType.Input.ToString());
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasKey(question => question.Id);
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RadioQuestion> RadioQuestions { get; set; }
        public virtual DbSet<CheckboxQuestion> CheckboxQuestions { get; set; }
        public virtual DbSet<InputQuestion> InputQuestions { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
    }
}
