using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class ChangeCategoryIdToRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Categories_CategoryId",
                table: "Publications");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Publications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0260cbfd-2a86-4cae-87fd-0001f98cdc7b", "AQAAAAEAACcQAAAAEKU8+mmg1ubDHbLNAEncg9teuhVT/yuWSSaJootu+eo3LjfPkYyDeCOezx4ASYcr9g==", "16c1e4de-7368-4181-a972-774c9abca484" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d97433b-7f15-464d-9689-29ea871c4cc2", "AQAAAAEAACcQAAAAEHLKjn/I51dXaSJ3IKxcaYeJB4/cH2RJXWcPEkScJRAEp7JpZ8QMEwazCMhipwU7jA==", "33c28143-6904-4cc7-80cb-3f0bae605529" });

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Categories_CategoryId",
                table: "Publications",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Categories_CategoryId",
                table: "Publications");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Publications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31734029-2580-4cd6-9458-aff32dff388b", "AQAAAAEAACcQAAAAENlauRJZhq/l94zsRTVzRPcjF7/zjxkoDJyjLeYokvGgHk519LNwwvkGitilR8U2Cg==", "6d3577f1-1870-4d91-981e-b29d569cf4a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68dd8a19-1a44-41ee-a943-6a2c5b48857c", "AQAAAAEAACcQAAAAEOcnKDGCkE3RaMyfdo+l1Om2rrBhgehCs7VVHXMJw8wAfStAc//9b41cGIf5iTRAgw==", "2a187d53-f372-495e-8e17-3464e137d82a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Categories_CategoryId",
                table: "Publications",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
