using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Infrastructure.Data.Migrations
{
    public partial class CommentPublicationIdRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e9afd66-e5f1-4cec-9e30-23edc686ea66", "AQAAAAEAACcQAAAAEFlH8x+VDQTPJA55jDEAA6cfVZaAYdMIKl8gsd9XEcOpUnm0i+JzV5bTDR/pjTQmpg==", "7f1e63e3-d553-4c5c-8fce-cec936e93454" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c277e29-377e-4722-960d-8fbb0dfa35c5", "AQAAAAEAACcQAAAAEISCEJvJ2ABmIh+HLYDJW647AEQHXpxhKMsP7nHCSzwp9/DysvoIM+ftui9t7W9Uaw==", "19f49a23-32e3-4108-8f96-b613940fe389" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
