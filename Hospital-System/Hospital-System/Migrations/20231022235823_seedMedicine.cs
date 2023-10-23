using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class seedMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedicinePortion",
                table: "MedicineMedicalReports",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7b44806-b6ed-4181-854d-23e9fb883ee9", "AQAAAAIAAYagAAAAEME42gN0WorL8tVZzJ+u+j5/3YcVbmOdGdCJLKrDzmrlXfmgjJ54IrrFIy0kG2w+oQ==", "5f79d2a2-a7a2-4fde-861f-2f34ffca6456" });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "MedicalReportId", "MedicineName", "Portion" },
                values: new object[,]
                {
                    { 1, null, "Ibuprofen", "200mg" },
                    { 2, null, "Paracetamol", "500mg" },
                    { 3, null, "Amoxicillin", "250mg" },
                    { 4, null, "Aspirin", "325mg" },
                    { 5, null, "Cetirizine", "10mg" },
                    { 6, null, "Diclofenac", "50mg" },
                    { 7, null, "Erythromycin", "500mg" },
                    { 8, null, "Furosemide", "40mg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<int>(
                name: "MedicinePortion",
                table: "MedicineMedicalReports",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc840a1d-97c5-4d8e-8dd4-e557efef0b10", "AQAAAAIAAYagAAAAEBCz//K2UDNUqlcp9au+jh+8ZmQKR4ugPEWyFDoT77FRK9rVn+kPOf2aRpmZi/UJhg==", "09e59986-328d-40b8-a7ea-ff35a745eeb9" });
        }
    }
}
