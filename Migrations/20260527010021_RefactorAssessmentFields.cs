using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabySteps.API.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAssessmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WakeUpPattern",
                table: "Assessments",
                newName: "WakeUpTime");

            migrationBuilder.RenameColumn(
                name: "StimulationLevel",
                table: "Assessments",
                newName: "NapFrequency");

            migrationBuilder.RenameColumn(
                name: "NapPattern",
                table: "Assessments",
                newName: "MedicalContext");

            migrationBuilder.RenameColumn(
                name: "Goal",
                table: "Assessments",
                newName: "ImprovementGoal");

            migrationBuilder.RenameColumn(
                name: "DiagnosisMedicationStatus",
                table: "Assessments",
                newName: "ActivityFrequency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WakeUpTime",
                table: "Assessments",
                newName: "WakeUpPattern");

            migrationBuilder.RenameColumn(
                name: "NapFrequency",
                table: "Assessments",
                newName: "StimulationLevel");

            migrationBuilder.RenameColumn(
                name: "MedicalContext",
                table: "Assessments",
                newName: "NapPattern");

            migrationBuilder.RenameColumn(
                name: "ImprovementGoal",
                table: "Assessments",
                newName: "Goal");

            migrationBuilder.RenameColumn(
                name: "ActivityFrequency",
                table: "Assessments",
                newName: "DiagnosisMedicationStatus");
        }
    }
}
