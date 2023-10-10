using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeatherApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "location_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_location_group_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "location_group_item",
                columns: table => new
                {
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    LocationGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location_group_item", x => new { x.Latitude, x.Longitude });
                    table.ForeignKey(
                        name: "FK_location_group_item_location_group_LocationGroupId",
                        column: x => x.LocationGroupId,
                        principalTable: "location_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "location_weather",
                columns: table => new
                {
                    LocationGroupItemLatitude = table.Column<decimal>(type: "numeric", nullable: false),
                    LocationGroupItemLongitude = table.Column<decimal>(type: "numeric", nullable: false),
                    latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    region = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    last_updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    temp_c = table.Column<decimal>(type: "numeric", nullable: false),
                    feels_like_c = table.Column<decimal>(type: "numeric", nullable: false),
                    condition_text = table.Column<string>(type: "text", nullable: false),
                    condition_image_url = table.Column<string>(type: "text", nullable: false),
                    condition_code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location_weather", x => new { x.LocationGroupItemLatitude, x.LocationGroupItemLongitude });
                    table.ForeignKey(
                        name: "FK_location_weather_location_group_item_LocationGroupItemLatit~",
                        columns: x => new { x.LocationGroupItemLatitude, x.LocationGroupItemLongitude },
                        principalTable: "location_group_item",
                        principalColumns: new[] { "Latitude", "Longitude" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_location_group_UserId",
                table: "location_group",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_location_group_item_LocationGroupId",
                table: "location_group_item",
                column: "LocationGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "location_weather");

            migrationBuilder.DropTable(
                name: "location_group_item");

            migrationBuilder.DropTable(
                name: "location_group");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
