using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabySteps.API.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBabyUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponsibleName",
                table: "Babies",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "CaregiverName",
                table: "Babies",
                newName: "RelationshipToBaby");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Babies",
                newName: "ResponsibleName");

            migrationBuilder.RenameColumn(
                name: "RelationshipToBaby",
                table: "Babies",
                newName: "CaregiverName");
        }
    }
}
