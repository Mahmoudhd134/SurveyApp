using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RmoveIsUniqeFromOptionColumnInSurveyOptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyOptions_Option",
                table: "SurveyOptions");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOptions_Option",
                table: "SurveyOptions",
                column: "Option");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyOptions_Option",
                table: "SurveyOptions");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOptions_Option",
                table: "SurveyOptions",
                column: "Option",
                unique: true);
        }
    }
}
