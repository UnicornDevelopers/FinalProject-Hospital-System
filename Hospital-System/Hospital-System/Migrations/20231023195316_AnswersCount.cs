using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class AnswersCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerCount",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83cd395e-cc5a-4137-86b0-e47d29eb2946", "AQAAAAIAAYagAAAAEGeP8Y9vVHKu4NQEyeG8oaz5ZPs6xXq1IpCOmcuxI0ZKxHbRLLe6fbVh9SLE5t7Yog==", "963cb977-911d-4ff4-806b-4e364b3a43ef" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerCount",
                table: "Questions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f1cd7dc-b0c6-4035-ba4d-46ad984f73b2", "AQAAAAIAAYagAAAAEJAldUW0wNyljPYFeoyP5DvFiMPyGZN5MPRnjh185O1+js7mL8m8MvDyf9Ho3TosSw==", "1da1bcac-cba0-4af9-a642-78685edcf627" });
        }
    }
}
