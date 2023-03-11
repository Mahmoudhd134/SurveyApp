using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdColumnInSurveyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Surveys",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_AspNetUsers_UserId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Surveys");
        }
    }
}
