using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class JoinTableMedical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Portion",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "MedicineMedicalReports",
                columns: table => new
                {
                    MedicalReportID = table.Column<int>(type: "int", nullable: false),
                    MedicineID = table.Column<int>(type: "int", nullable: false),
                    MedicinePortion = table.Column<int>(type: "int", nullable: false),
                    TimesInDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineMedicalReports", x => new { x.MedicalReportID, x.MedicineID });
                    table.ForeignKey(
                        name: "FK_MedicineMedicalReports_MedicalReports_MedicalReportID",
                        column: x => x.MedicalReportID,
                        principalTable: "MedicalReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineMedicalReports_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc840a1d-97c5-4d8e-8dd4-e557efef0b10", "AQAAAAIAAYagAAAAEBCz//K2UDNUqlcp9au+jh+8ZmQKR4ugPEWyFDoT77FRK9rVn+kPOf2aRpmZi/UJhg==", "09e59986-328d-40b8-a7ea-ff35a745eeb9" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineMedicalReports_MedicineID",
                table: "MedicineMedicalReports",
                column: "MedicineID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineMedicalReports");

            migrationBuilder.AlterColumn<string>(
                name: "Portion",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "245229e8-b989-4d2a-8f96-64b9a597f040", "AQAAAAIAAYagAAAAEPS1ejh046VxcdInTVbdB/JHg40a1mTZMnb1ElwCK5PovfU4uhl8W7Mrx23XXBqfHw==", "93b2d4fd-1752-43e2-a1a7-812b674dc7d9" });
        }
    }
}
