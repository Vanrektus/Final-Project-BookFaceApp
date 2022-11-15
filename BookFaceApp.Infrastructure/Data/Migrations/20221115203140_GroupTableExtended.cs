using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class GroupTableExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Group",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Group",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Group_UserId",
                table: "Group",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_AspNetUsers_UserId",
                table: "Group",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_AspNetUsers_UserId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_UserId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Group");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "670c6fdf-6992-49f8-95dd-abf4016c5d97", "AQAAAAEAACcQAAAAED0uTdEZex925WjFctghnmvbT3uVYGWtltHZYnfCSh0x4f8RjmgBfOv15uUTfK4Vng==", "5f09d7bf-5180-412a-8cfd-0d9758e23c04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dee4e0cd-cc06-4fda-92fa-18feeddd9f96", "AQAAAAEAACcQAAAAEOmAn47hse2CYSGxAB6LjIA3wX0Hx+i3GvgNJNehbbz5PYX7Zd5AEN0tnThNRVRz9Q==", "8f720d9b-d94f-4b79-87e3-ebf3d458dcc0" });
        }
    }
}
