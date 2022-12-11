using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class UserProfilePictureIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "a40b9cf5-e997-4639-a551-c3a374eb388c", "AQAAAAEAACcQAAAAEP+cmdHkP6SimR0lqBUhoZ1JCK1Vaq+rba0+xB/AU27CcRdxK9utZ0yAAVOwS8vLgA==", "b7b042f3-6b16-4c71-b298-811f8e7e0b55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdfdafb4-4d76-466d-b365-88e911ea0ad3", "AQAAAAEAACcQAAAAED2gdD3incjh2OweMKaEf16wL0VUaULa65a+wcjpZ6cJayVOuquveNMQlhqGtjG0AA==", "ba5e4163-0988-4be3-bece-281f9f76673f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0aa95ac-e75f-4f89-9ff3-c7d9b4e04d83", "AQAAAAEAACcQAAAAEAjt0i5fYVN/M9xREc5CEUKWCaDya8RXYV5IWp3r3ltO+Aj2Zv6aLaTbntToIliglA==", "f71dd266-9889-4e3c-ad1a-da10e88b94e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba477f08-3286-47a6-b617-7f2c222f8959", "AQAAAAEAACcQAAAAEKFREHhOCamXideASI+PEEdEq+II+CYguwrpJ7lt125uEFd7W8DxDQZEAVQDCZRqkw==", "e0f4e2a0-90b6-45e4-a6d1-b584e149dec6" });
        }
    }
}
