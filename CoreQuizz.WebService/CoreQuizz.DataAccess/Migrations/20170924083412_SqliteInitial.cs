using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreQuizz.DataAccess.Migrations
{
    public partial class SqliteInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomGroupGrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GrantedUserId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomGroupGrant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedById = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GroupBelongedId = table.Column<int>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    SurveyPassAccessLevel = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    QuestionLabel = table.Column<string>(nullable: true),
                    ResultId = table.Column<int>(nullable: true),
                    SurveyId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    QuestionId = table.Column<int>(nullable: true),
                    RadioQuestionId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_RadioQuestionId",
                        column: x => x.RadioQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CustomGroupId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    ModifieDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_RadioQuestionId",
                table: "QuestionOptions",
                column: "RadioQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreatedById",
                table: "Surveys",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_GroupBelongedId",
                table: "Surveys",
                column: "GroupBelongedId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomGroupId",
                table: "Users",
                column: "CustomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomGroupGrant_Users_GrantedUserId",
                table: "CustomGroupGrant",
                column: "GrantedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomGroupGrant_CustomGroup_GroupId",
                table: "CustomGroupGrant",
                column: "GroupId",
                principalTable: "CustomGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_CreatedById",
                table: "Surveys",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_CustomGroup_Users_UserId",
                table: "CustomGroup");

            migrationBuilder.DropTable(
                name: "CustomGroupGrant");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "SurveyGrant");

            migrationBuilder.DropTable(
                name: "SurveyStar");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CustomGroup");
        }
    }
}
