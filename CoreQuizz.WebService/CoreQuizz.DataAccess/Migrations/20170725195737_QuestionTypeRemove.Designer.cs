using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreQuizz.DataAccess.DbContext;

namespace CoreQuizz.DataAccess.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("20170725195737_QuestionTypeRemove")]
    partial class QuestionTypeRemove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("QuestionLabel");

                    b.Property<int?>("ResultId");

                    b.Property<int?>("SurveyId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");

                    b.HasDiscriminator<string>("Type").HasValue("Question");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool?>("IsSelected");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<int?>("QuestionId");

                    b.Property<int?>("RadioQuestionId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("RadioQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.Survey", b =>
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

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.User", b =>
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

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.CheckboxQuestion", b =>
                {
                    b.HasBaseType("CoreQuizz.Shared.DomainModel.Question");


                    b.ToTable("CheckboxQuestion");

                    b.HasDiscriminator().HasValue("Checkbox");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.InputQuestion", b =>
                {
                    b.HasBaseType("CoreQuizz.Shared.DomainModel.Question");

                    b.Property<string>("Value");

                    b.ToTable("InputQuestion");

                    b.HasDiscriminator().HasValue("Input");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.RadioQuestion", b =>
                {
                    b.HasBaseType("CoreQuizz.Shared.DomainModel.Question");


                    b.ToTable("RadioQuestion");

                    b.HasDiscriminator().HasValue("Radio");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.Question", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.QuestionOption", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.CheckboxQuestion", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId");

                    b.HasOne("CoreQuizz.Shared.DomainModel.RadioQuestion")
                        .WithMany("Options")
                        .HasForeignKey("RadioQuestionId");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.Survey", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.User", "CreatedBy")
                        .WithMany("Surveys")
                        .HasForeignKey("CreatedById");
                });
        }
    }
}
