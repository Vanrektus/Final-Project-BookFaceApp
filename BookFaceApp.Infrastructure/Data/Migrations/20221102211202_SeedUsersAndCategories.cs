using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class SeedUsersAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "31734029-2580-4cd6-9458-aff32dff388b", "guest@mail.com", false, "Gostin", "Gostinov", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAENlauRJZhq/l94zsRTVzRPcjF7/zjxkoDJyjLeYokvGgHk519LNwwvkGitilR8U2Cg==", null, false, "6d3577f1-1870-4d91-981e-b29d569cf4a0", false, "Guest" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "68dd8a19-1a44-41ee-a943-6a2c5b48857c", "admin@mail.com", false, "Vancho", "Vanchov", false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOcnKDGCkE3RaMyfdo+l1Om2rrBhgehCs7VVHXMJw8wAfStAc//9b41cGIf5iTRAgw==", null, false, "2a187d53-f372-495e-8e17-3464e137d82a", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fun" },
                    { 2, "Animals" },
                    { 3, "Cars" },
                    { 4, "Politics" },
                    { 5, "Games" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
