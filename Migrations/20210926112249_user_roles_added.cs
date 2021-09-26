using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAndBeauty.Migrations
{
    public partial class user_roles_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "0cd99832-ddd2-4033-94b9-ad583d630767", "admin", "Administrator" },
                    { "2", "0f54f3e5-ee5f-44fb-ac5c-6655e7e57a60", "manager", "Manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
