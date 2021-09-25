using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAndBeauty.Migrations
{
    public partial class seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "food_set",
                column: "Id",
                values: new object[]
                {
                    1,
                    2
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "food_set",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "food_set",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
