using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class RemovedUnnecessaryProfilePicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "321da845-7d39-4b91-b5f1-e5dd7b25ca7f", "AQAAAAEAACcQAAAAEEbArA58XAPechz5SR+Xf5Z89CTpJ1TMXrmNO0KVXxsedSR08FZ1+hiz68/4RGLT4g==", "1e12337e-eb79-44d4-b2c1-f2ebcb6f62c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8a0bdd7-65cd-436c-ac6c-de534f76ddf9", "AQAAAAEAACcQAAAAEMxjIjZclyq7ErC8Ba9JiITjJKisQa2wfOiXhz+orPLsdr66Z9W1bg4g48DS5R4Z8g==", "2333b76f-2b34-4156-9f6e-9e6b8f051d08" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "861e6eb9-1614-4508-8fab-84175df7c6ef", "AQAAAAEAACcQAAAAEMPZ0yNSNczFkGk//vWXNkGlDQPgMmJPwY3uIhpS4yDOEyDyY17JIc7lLj49k+OJsQ==", "d6fa7372-beca-427d-b218-366c98be7162" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f54a522-ea7d-4f27-a009-6ef85259aad2", "AQAAAAEAACcQAAAAEHVkRpgxRHBp+VtUhp3iufh2GquXeWemFLB9DlEjJS3OGMCOfeFM246q5gx/Q+y73Q==", "a64f5b8d-ca1e-4ec8-8444-7bf6dfff8024" });
        }
    }
}
