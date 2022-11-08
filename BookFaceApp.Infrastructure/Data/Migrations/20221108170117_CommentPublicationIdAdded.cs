using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class CommentPublicationIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06db6247-b503-4089-8bdc-0bc678479b2a", "AQAAAAEAACcQAAAAEJYhPzLqLSD3RAHQt6HhYLD328txYEVY36cuIR1AQtLPIUPcFgOT8yG4Ny/B6wIGJA==", "b4bbc832-c2f3-4ee0-aef6-331122c1224c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "523a248d-2417-4df1-982d-6cc005159a62", "AQAAAAEAACcQAAAAEDicBvC96ZM9ZKp2DEYIFR9KMqrWy68faoy2QRYflXXyTyi7Hm/CVTUpv3aq4QsEjw==", "7fb37429-8715-43b8-944a-66adc6bd65b8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Comments");

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
        }
    }
}
