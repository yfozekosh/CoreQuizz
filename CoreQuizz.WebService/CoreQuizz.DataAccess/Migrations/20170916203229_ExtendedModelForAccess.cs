using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreQuizz.DataAccess.Migrations
{
    public partial class ExtendedModelForAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomGroupId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Surveys",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupBelongedId",
                table: "Surveys",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurveyPassAccessLevel",
                table: "Surveys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomGroup_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyGrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessLevel = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GrantedUserId = table.Column<int>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    SurveyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyGrant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyGrant_Users_GrantedUserId",
                        column: x => x.GrantedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyGrant_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyStar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LeftById = table.Column<int>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    SurveyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyStar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyStar_Users_LeftById",
                        column: x => x.LeftById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyStar_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomGroupGrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GrantedUserId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomGroupGrant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomGroupGrant_Users_GrantedUserId",
                        column: x => x.GrantedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomGroupGrant_CustomGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CustomGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomGroupId",
                table: "Users",
                column: "CustomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_GroupBelongedId",
                table: "Surveys",
                column: "GroupBelongedId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomGroup_UserId",
                table: "CustomGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomGroupGrant_GrantedUserId",
                table: "CustomGroupGrant",
                column: "GrantedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomGroupGrant_GroupId",
                table: "CustomGroupGrant",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyGrant_GrantedUserId",
                table: "SurveyGrant",
                column: "GrantedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyGrant_SurveyId",
                table: "SurveyGrant",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyStar_LeftById",
                table: "SurveyStar",
                column: "LeftById");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyStar_SurveyId",
                table: "SurveyStar",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_CustomGroup_GroupBelongedId",
                table: "Surveys",
                column: "GroupBelongedId",
                principalTable: "CustomGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CustomGroup_CustomGroupId",
                table: "Users",
                column: "CustomGroupId",
                principalTable: "CustomGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_CustomGroup_GroupBelongedId",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CustomGroup_CustomGroupId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CustomGroupGrant");

            migrationBuilder.DropTable(
                name: "SurveyGrant");

            migrationBuilder.DropTable(
                name: "SurveyStar");

            migrationBuilder.DropTable(
                name: "CustomGroup");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_GroupBelongedId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "CustomGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "GroupBelongedId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyPassAccessLevel",
                table: "Surveys");
        }
    }
}
