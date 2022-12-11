using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class ProfilePicturesFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ApplicationFile_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ApplicationFile");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePictures");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePictures",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePictures");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationFile", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33f5a89f-388b-4abd-8941-24d6acd9c9e1", "AQAAAAEAACcQAAAAEFxAoHiaPD324Od1wY8q98lgq7YwHWFNN/IK7rVVgTfBADnmhgU0T4h1EAajkvwvPg==", "ef80b571-eb2c-484e-94ec-6afc8475af27" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c50b63c7-15cb-4fdc-b290-19a39ef67090", "AQAAAAEAACcQAAAAEAJOYIr42E8EKvvmtLZ+xXQABRRpYmGpPWF0KeuARi0FsQ4GIUO40IIqPUZgusAMaw==", "723a648b-0bff-465b-88bc-ec955f9e394f" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePictures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ApplicationFile_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "ApplicationFile",
                principalColumn: "Id");
        }
    }
}
