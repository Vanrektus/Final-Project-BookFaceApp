using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class FilesTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eceaded1-caf2-4c46-8e37-baf27409de7e", "AQAAAAEAACcQAAAAEL9Pr+hd0BqRrZM7bHK1ZY+jmaIaqooIaAawGp7BlhSmG83Hpfv+1eEAYEZINdPTrA==", "f11301bf-1215-4a03-965f-4336a1a57cd9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c468993-e206-450a-829b-0f2eaf27c967", "AQAAAAEAACcQAAAAEP4t8+hnATxyBaECqn4C4lVldjtyZYbwoqMiaoVqB1Z7esbpTtafGr46JX46hwW6Gg==", "5a9e5b4f-839b-46d2-bfb4-a949507f8ffa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d662407a-f3cc-4246-9b49-40b63424c7eb", "AQAAAAEAACcQAAAAENFZEbt3BP/xv4+LguW+cRLxIacnCYL9ESHR7G3NmDbCYadpOjzXlkYf1iSjOsjRpw==", "e4fc3bf4-76da-4a08-87ef-779116f677d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e12b09e8-d035-4d8f-a12a-6286eea91c74", "AQAAAAEAACcQAAAAEG2nvcPoKj52PC0a/aCnCBFAdnxmK0rHQVJeuvlDWMrFcyb5Ei8xFG7iBsUOZ3ZHow==", "820497d6-d23f-4a4b-8a5a-8e6061e16e04" });
        }
    }
}
