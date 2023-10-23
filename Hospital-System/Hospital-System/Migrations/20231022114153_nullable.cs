using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52a45144-dbd5-4c1b-a178-a20f57ae8b01", "AQAAAAIAAYagAAAAEJLoYKnLO1POnEuU8Mw854zTxkUNhseWh53rpEa7yeSyihYg1qkm0GMl//ruweO+lQ==", "43fc66f7-d2d6-42fd-a6b4-e7c2e251429b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "957f9cd1-a86c-42c6-aa9b-6ca2921ef1f8", "AQAAAAIAAYagAAAAEGcYshjWE9ZI6sKHHsO0MizA+XHy/cbuJaxSfD4HnUBRlYYolhro5GRSrkqGX45mcQ==", "c05f7792-89c2-445c-85dd-9dbd99d87859" });
        }
    }
}
