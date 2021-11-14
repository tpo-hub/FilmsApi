using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace FilmsApi.Migrations
{
    public partial class addGeoLocationToCinema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "location",
                table: "Cinema",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "Cinema");
        }
    }
}
