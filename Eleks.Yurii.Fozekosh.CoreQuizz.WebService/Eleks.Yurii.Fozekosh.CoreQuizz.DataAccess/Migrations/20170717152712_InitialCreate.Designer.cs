using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("20170717152712_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("SecondName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
