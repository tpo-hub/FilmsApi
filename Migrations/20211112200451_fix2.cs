using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsApi.Migrations
{
    public partial class fix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coment = table.Column<string>(nullable: true),
                    Punctuation = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a9c6664-b191-4920-ab31-9c0968f2c409",
                column: "ConcurrencyStamp",
                value: "479b0312-2ee8-4072-8f39-b95123d2592d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86fc889a-77e2-4ca2-91b4-ac86857a2edc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e428d6e8-272c-40a3-bba3-77bdb416a397", "AQAAAAEAACcQAAAAEJpVWdmxsrOLoQcoCa1WHVS7u8u1juCS0Hm+uGv4joM9q8dMMTJD5720sq1GtR8M+g==", "927098e8-7c55-49a9-9540-8b16b1ba0885" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a9c6664-b191-4920-ab31-9c0968f2c409",
                column: "ConcurrencyStamp",
                value: "4f90eec7-5ea6-452f-ad03-ad34c4801964");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86fc889a-77e2-4ca2-91b4-ac86857a2edc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc41887e-5ed2-42d2-95f3-599c88c05a10", "AQAAAAEAACcQAAAAECBHFhJYfUFzJlQfBjoMS6m9jzVxXwY/cbLMi/H3Y9zTc9ZXcXDCbOzgclweIBa6Pg==", "8e1eafae-588a-42f0-9ae4-61014653b97c" });
        }
    }
}
