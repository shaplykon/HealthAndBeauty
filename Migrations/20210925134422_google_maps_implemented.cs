using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HealthAndBeauty.Migrations
{
    public partial class google_maps_implemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "google_maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longtitude = table.Column<double>(type: "double precision", nullable: false),
                    address_name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_google_maps", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "google_maps",
                columns: new[] { "Id", "address_name", "description", "latitude", "longtitude" },
                values: new object[] { 1, "SF", "San Francisco", 37.770000000000003, -122.41 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "google_maps");
        }
    }
}
