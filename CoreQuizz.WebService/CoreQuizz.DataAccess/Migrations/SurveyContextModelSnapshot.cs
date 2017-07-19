using System;
using CoreQuizz.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreQuizz.DataAccess.Migrations
{
    [DbContext(typeof(SurveyContext))]
    partial class SurveyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("QuestionLabel");

                    b.Property<int>("QuestionType");

                    b.Property<int?>("ResultId");

                    b.Property<int?>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Question");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CheckboxQuestionId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool?>("IsSelected");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<int?>("QuestionId");

                    b.Property<int?>("RadioQuestionId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CheckboxQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("RadioQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CreatedById");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Salt");

                    b.Property<string>("SecondName");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.CheckboxQuestion", b =>
                {
                    b.HasBaseType("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Question");


                    b.ToTable("CheckboxQuestion");

                    b.HasDiscriminator().HasValue("CheckboxQuestion");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.InputQuestion", b =>
                {
                    b.HasBaseType("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Question");

                    b.Property<string>("Value");

                    b.ToTable("InputQuestion");

                    b.HasDiscriminator().HasValue("InputQuestion");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.RadioQuestion", b =>
                {
                    b.HasBaseType("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Question");


                    b.ToTable("RadioQuestion");

                    b.HasDiscriminator().HasValue("RadioQuestion");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Question", b =>
                {
                    b.HasOne("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.QuestionOption", b =>
                {
                    b.HasOne("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.CheckboxQuestion")
                        .WithMany("Options")
                        .HasForeignKey("CheckboxQuestionId");

                    b.HasOne("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.RadioQuestion")
                        .WithMany("Options")
                        .HasForeignKey("RadioQuestionId");
                });

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.Survey", b =>
                {
                    b.HasOne("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.User", "CreatedBy")
                        .WithMany("Surveys")
                        .HasForeignKey("CreatedById");
                });
        }
    }
}
