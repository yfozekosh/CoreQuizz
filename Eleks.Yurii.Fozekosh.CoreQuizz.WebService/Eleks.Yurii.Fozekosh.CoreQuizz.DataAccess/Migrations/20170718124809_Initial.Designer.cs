using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.DbContext;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("20170718124809_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Id");

                    b.Property<string>("MiddleName");

                    b.Property<DateTime>("ModifieDateTime");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Salt");

                    b.Property<string>("SecondName");

                    b.HasKey("Email");

                    b.HasAlternateKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
