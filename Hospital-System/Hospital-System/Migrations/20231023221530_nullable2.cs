using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class nullable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AnswerCount",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79b18fc4-1c9b-497b-a228-8945a68c3cad", "AQAAAAIAAYagAAAAED2sCVohl3lMXO9ALEfJcj68xHLzAwRFTw2MW3m3shuTvICG6QjEDJGdU9lE1Y0aEw==", "afdf880c-f531-4073-a57b-92327c451d53" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AnswerCount",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83cd395e-cc5a-4137-86b0-e47d29eb2946", "AQAAAAIAAYagAAAAEGeP8Y9vVHKu4NQEyeG8oaz5ZPs6xXq1IpCOmcuxI0ZKxHbRLLe6fbVh9SLE5t7Yog==", "963cb977-911d-4ff4-806b-4e364b3a43ef" });
        }
    }
}
