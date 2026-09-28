using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabySteps.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSleepTimeToAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SleepTime",
                table: "Assessments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SleepTime",
                table: "Assessments");
        }
    }
}
