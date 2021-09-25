using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAndBeauty.Migrations
{
    public partial class table_name_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_article",
                table: "article");

            migrationBuilder.RenameTable(
                name: "article",
                newName: "food_set");

            migrationBuilder.AddPrimaryKey(
                name: "PK_food_set",
                table: "food_set",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_food_set",
                table: "food_set");

            migrationBuilder.RenameTable(
                name: "food_set",
                newName: "article");

            migrationBuilder.AddPrimaryKey(
                name: "PK_article",
                table: "article",
                column: "Id");
        }
    }
}
