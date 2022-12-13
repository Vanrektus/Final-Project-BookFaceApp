using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class ProfilePictureImageToStringAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageToString",
                table: "ProfilePictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageToString",
                table: "ProfilePictures");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a40b9cf5-e997-4639-a551-c3a374eb388c", "AQAAAAEAACcQAAAAEP+cmdHkP6SimR0lqBUhoZ1JCK1Vaq+rba0+xB/AU27CcRdxK9utZ0yAAVOwS8vLgA==", "b7b042f3-6b16-4c71-b298-811f8e7e0b55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdfdafb4-4d76-466d-b365-88e911ea0ad3", "AQAAAAEAACcQAAAAED2gdD3incjh2OweMKaEf16wL0VUaULa65a+wcjpZ6cJayVOuquveNMQlhqGtjG0AA==", "ba5e4163-0988-4be3-bece-281f9f76673f" });
        }
    }
}
