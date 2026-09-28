using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BabySteps.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateAssessments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BabyId = table.Column<int>(type: "integer", nullable: false),
                    ExpectedBirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DiagnosisMedicationStatus = table.Column<string>(type: "text", nullable: false),
                    FeedingType = table.Column<string>(type: "text", nullable: false),
                    FeedingFrequency = table.Column<string>(type: "text", nullable: false),
                    WakeUpPattern = table.Column<string>(type: "text", nullable: false),
                    NapPattern = table.Column<string>(type: "text", nullable: false),
                    NightWakeFrequency = table.Column<string>(type: "text", nullable: false),
                    StimulationLevel = table.Column<string>(type: "text", nullable: false),
                    Goal = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_Babies_BabyId",
                        column: x => x.BabyId,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_BabyId",
                table: "Assessments",
                column: "BabyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assessments");
        }
    }
}
