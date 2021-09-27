using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAndBeauty.Migrations
{
    public partial class addresses_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "52618289-c979-4e8c-9790-4164db554236");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e28ef028-33f5-47f3-a65e-88677b4964b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "454e22d2-4fd8-4169-836c-32a39825c575");

            migrationBuilder.UpdateData(
                table: "google_maps",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "address_name", "description", "latitude", "longtitude" },
                values: new object[] { "9 Gikalo str., Minsk, Belarus", "Minsk", 53.912254769620034, 27.594474988468278 });

            migrationBuilder.InsertData(
                table: "google_maps",
                columns: new[] { "Id", "address_name", "description", "latitude", "longtitude" },
                values: new object[] { 2, "65 Nezavisimosti Ave., Minsk", "Minsk", 53.921072362950333, 27.592836631586675 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "google_maps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "6ebbe944-06a6-41d4-be54-780dd0366afa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2099b946-5cd7-4f0f-82c6-ba73487ac4f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "43c859f5-bb3c-42dc-8a0e-205b0e592577");

            migrationBuilder.UpdateData(
                table: "google_maps",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "address_name", "description", "latitude", "longtitude" },
                values: new object[] { "SF", "San Francisco", 37.770000000000003, -122.41 });
        }
    }
}
