using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.DataAccess.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("20170916203229_ExtendedModelForAccess")]
    partial class ExtendedModelForAccess
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.CustomGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CustomGroup");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.CustomGroupGrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("GrantedUserId");

                    b.Property<int?>("GroupId");

                    b.Property<DateTime>("ModifieDateTime");

                    b.HasKey("Id");

                    b.HasIndex("GrantedUserId");

                    b.HasIndex("GroupId");

                    b.ToTable("CustomGroupGrant");
                });

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

                    b.Property<string>("Description");

                    b.Property<int?>("GroupBelongedId");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<int>("SurveyPassAccessLevel");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("GroupBelongedId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.SurveyGrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessLevel");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("GrantedUserId");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<int?>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("GrantedUserId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyGrant");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.SurveyStar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("LeftById");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<int?>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("LeftById");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyStar");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CustomGroupId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("Name");

                    b.Property<string>("SecondName");

                    b.HasKey("Id");

                    b.HasIndex("CustomGroupId");

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

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.CustomGroup", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.CustomGroupGrant", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.User", "GrantedUser")
                        .WithMany()
                        .HasForeignKey("GrantedUserId");

                    b.HasOne("CoreQuizz.Shared.DomainModel.CustomGroup", "Group")
                        .WithMany("Grants")
                        .HasForeignKey("GroupId");
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

                    b.HasOne("CoreQuizz.Shared.DomainModel.CustomGroup", "GroupBelonged")
                        .WithMany()
                        .HasForeignKey("GroupBelongedId");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.SurveyGrant", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.User", "GrantedUser")
                        .WithMany()
                        .HasForeignKey("GrantedUserId");

                    b.HasOne("CoreQuizz.Shared.DomainModel.Survey", "Survey")
                        .WithMany("Grants")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.SurveyStar", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.User", "LeftBy")
                        .WithMany("Stars")
                        .HasForeignKey("LeftById");

                    b.HasOne("CoreQuizz.Shared.DomainModel.Survey", "Survey")
                        .WithMany("Stars")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("CoreQuizz.Shared.DomainModel.User", b =>
                {
                    b.HasOne("CoreQuizz.Shared.DomainModel.CustomGroup")
                        .WithMany("UsersInGroup")
                        .HasForeignKey("CustomGroupId");
                });
        }
    }
}
