using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class NewCategorySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63023590-b7a1-4faf-a28a-74b855a810fc", "AQAAAAEAACcQAAAAEOppKHii2RV+WsB4JpOyeOqCbqIcex2r0L1+HEHkr5khWgG+a1cS0Dya2QDjtJgHRw==", "96debf40-ac76-4be3-998c-2b44ea3bb59f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "558a7f35-47fa-4c63-bb05-05e2460a51d4", "AQAAAAEAACcQAAAAEKS84HWIFEwUHwbj49iuPToHHT/5gn9+4K67d2V79yXP3SWCqangrOcB8RQXcySOgA==", "9432e472-1040-48f9-8b01-3d46541bb2fb" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Food" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

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
        }
    }
}
