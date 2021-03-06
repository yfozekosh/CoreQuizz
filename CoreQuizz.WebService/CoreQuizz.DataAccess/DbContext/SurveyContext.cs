﻿using CoreQuizz.Shared.DomainModel;
using CoreQuizz.Shared.DomainModel.Enum;
using CoreQuizz.Shared.DomainModel.Group;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
                entity.HasMany(x => x.Groups);
                entity.HasMany(x => x.Stars).WithOne(star => star.LeftBy);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(survey => survey.Id);
                entity.HasMany(survey => survey.Questions).WithOne(question => question.Survey).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(survey => survey.Grants).WithOne(grant => grant.Survey).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(survey => survey.Stars).WithOne(star => star.Survey).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CustomGroup>(entity =>
            {
                entity.HasKey(group => group.Id);
                entity.HasMany(group => group.UsersInGroup);
            });
            
            modelBuilder.Entity<SurveyGrant>().HasKey(grant => grant.Id);
            modelBuilder.Entity<CustomGroupGrant>().HasKey(grant => grant.Id);
            
            modelBuilder.Entity<Question>().HasKey(question => question.Id);
            
            modelBuilder.Entity<Question>().HasDiscriminator<string>("Type");

            modelBuilder.Entity<RadioQuestion>().HasBaseType<Question>();

            modelBuilder.Entity<RadioQuestion>(entity =>
            {
                entity.HasBaseType<Question>();
                entity.HasMany(question => question.Options).WithOne(option => (RadioQuestion)option.Question).OnDelete(DeleteBehavior.Cascade);
                
                entity.HasDiscriminator<string>("Type").HasValue("radio");
            });

            modelBuilder.Entity<CheckboxQuestion>(entity =>
            {
                entity.HasBaseType<Question>();
                entity.HasMany(question => question.Options).WithOne(option => (CheckboxQuestion) option.Question).OnDelete(DeleteBehavior.Cascade);
                entity.HasDiscriminator<string>("Type").HasValue("checkbox");
            });

            modelBuilder.Entity<InputQuestion>(entity =>
            {
                entity.HasBaseType<Question>();
                entity.HasDiscriminator<string>("Type").HasValue("input");
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
