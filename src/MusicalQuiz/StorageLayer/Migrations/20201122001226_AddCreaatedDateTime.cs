using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StorageLayer.Migrations
{
    public partial class AddCreaatedDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizSong_Quizzes_QuizId",
                table: "QuizSong");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizSong_Songs_SongId",
                table: "QuizSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizSong",
                table: "QuizSong");

            migrationBuilder.RenameTable(
                name: "QuizSong",
                newName: "QuizzesSongs");

            migrationBuilder.RenameIndex(
                name: "IX_QuizSong_SongId",
                table: "QuizzesSongs",
                newName: "IX_QuizzesSongs_SongId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Scores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Quizzes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizzesSongs",
                table: "QuizzesSongs",
                columns: new[] { "QuizId", "SongId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesSongs_Quizzes_QuizId",
                table: "QuizzesSongs",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizzesSongs_Songs_SongId",
                table: "QuizzesSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesSongs_Quizzes_QuizId",
                table: "QuizzesSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizzesSongs_Songs_SongId",
                table: "QuizzesSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizzesSongs",
                table: "QuizzesSongs");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Quizzes");

            migrationBuilder.RenameTable(
                name: "QuizzesSongs",
                newName: "QuizSong");

            migrationBuilder.RenameIndex(
                name: "IX_QuizzesSongs_SongId",
                table: "QuizSong",
                newName: "IX_QuizSong_SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizSong",
                table: "QuizSong",
                columns: new[] { "QuizId", "SongId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizSong_Quizzes_QuizId",
                table: "QuizSong",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizSong_Songs_SongId",
                table: "QuizSong",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
