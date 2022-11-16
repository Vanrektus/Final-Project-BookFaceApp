using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class PublicationGroupIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f65b624-38a1-4f9a-a347-50343e884461", "AQAAAAEAACcQAAAAEBFE34XGzkXVdc38DoAbG08AYnQNpMskLKPtq6KyOk1gDrW+9X0yfqC5sBuP2UlSqg==", "bdeb0d7d-7150-48c6-ae03-c92bfe5c592d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "547b92d9-4aad-4c2e-88c4-17484e3977f5", "AQAAAAEAACcQAAAAEKAQMN1k8qjP6jcSYcSEo/gqBizRyUyvXvAOIwYPB2UX2Y+YgNyEeOSWHOJTLkGLoA==", "f97e8623-0411-4440-b00e-71a594fb98a6" });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_GroupId",
                table: "Publications",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Group_GroupId",
                table: "Publications",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Group_GroupId",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_GroupId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Publications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49e989b5-dae5-49a2-85de-250db4e1e0ed", "AQAAAAEAACcQAAAAECRHDMCCKlWzYllup+qx/0jsVllgaVBDAJZkJ9zl6lQOS3+DiHvLCAWD6s2wtT6Xeg==", "4d6d5ee0-f104-485a-a794-dc0ec320153f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b45be553-c9d8-46e9-a965-f29431ed7bca", "AQAAAAEAACcQAAAAEOqka7g7gnmwLUegBnDiFX8RJ22zRPYIiJOaiAC7eRKPCSVqIangaNePAwU4a5eKRQ==", "79e01f21-a74b-4feb-be1b-f5cdf49be7f9" });
        }
    }
}
