using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AnswerCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50ab61c8-1790-40ca-899d-51042cc52e25", "AQAAAAIAAYagAAAAEPjv9BN8nZ7DaLzb0DhpizoyLF29acw+ubuRxTho/E2zvLocr5eylfGnBsLwQcNjcQ==", "b93fff66-83df-49d8-acd7-462e2b30fe17" });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_DoctorId",
                table: "Answers",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PatientId",
                table: "Questions",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c3b6fc2-7764-46ed-9673-64332b0fff86", "AQAAAAIAAYagAAAAEAx7ap3AvkB6gaZvmi9pwDmLOC7FnMgaG1o5b9j6mfmSk3Upj5Ae9Unbs604ubwThA==", "01a08462-bab4-436c-a069-79014d7dbe87" });
        }
    }
}
