using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class PublicationGroupRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationsGroups");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0817c328-5a70-429f-bdfb-1af4b7947aa9", "AQAAAAEAACcQAAAAEFN9jbNrRRAQCaHz/XNhGBNAa6nbwpzrbg6EZZnzOAgdWl/Chx2SaX6F5GaJtQ8crw==", "665a081f-4f92-47a1-9d5a-c5be61f7d3f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d028ac0-3414-4da6-aa0b-e69b28040743", "AQAAAAEAACcQAAAAEF0mEemeYFyqo+fF3Y9WWqwR3a63c3PZK3ElfIjrFln6JwaIq44jwQiNGufIKLJe8A==", "4ebad136-d7dc-463f-94b0-090bffc13c3c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicationsGroups",
                columns: table => new
                {
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationsGroups", x => new { x.PublicationId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_PublicationsGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicationsGroups_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_PublicationsGroups_GroupId",
                table: "PublicationsGroups",
                column: "GroupId");
        }
    }
}
