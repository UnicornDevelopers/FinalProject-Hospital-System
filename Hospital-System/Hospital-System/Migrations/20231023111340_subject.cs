using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class subject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f1cd7dc-b0c6-4035-ba4d-46ad984f73b2", "AQAAAAIAAYagAAAAEJAldUW0wNyljPYFeoyP5DvFiMPyGZN5MPRnjh185O1+js7mL8m8MvDyf9Ho3TosSw==", "1da1bcac-cba0-4af9-a642-78685edcf627" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Questions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52a45144-dbd5-4c1b-a178-a20f57ae8b01", "AQAAAAIAAYagAAAAEJLoYKnLO1POnEuU8Mw854zTxkUNhseWh53rpEa7yeSyihYg1qkm0GMl//ruweO+lQ==", "43fc66f7-d2d6-42fd-a6b4-e7c2e251429b" });
        }
    }
}
