using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsApi.Migrations
{
    public partial class addInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a9c6664-b191-4920-ab31-9c0968f2c409", "4f90eec7-5ea6-452f-ad03-ad34c4801964", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "86fc889a-77e2-4ca2-91b4-ac86857a2edc", 0, "dc41887e-5ed2-42d2-95f3-599c88c05a10", null, false, false, null, "example@example.com", "example@example.com", "AQAAAAEAACcQAAAAECBHFhJYfUFzJlQfBjoMS6m9jzVxXwY/cbLMi/H3Y9zTc9ZXcXDCbOzgclweIBa6Pg==", null, false, "8e1eafae-588a-42f0-9ae4-61014653b97c", false, "example@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "86fc889a-77e2-4ca2-91b4-ac86857a2edc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a9c6664-b191-4920-ab31-9c0968f2c409");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86fc889a-77e2-4ca2-91b4-ac86857a2edc");
        }
    }
}
