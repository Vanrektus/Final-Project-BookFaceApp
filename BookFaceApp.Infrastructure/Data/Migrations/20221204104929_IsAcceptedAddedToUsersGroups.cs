using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class IsAcceptedAddedToUsersGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "UsersGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "UsersGroups");

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
    }
}
