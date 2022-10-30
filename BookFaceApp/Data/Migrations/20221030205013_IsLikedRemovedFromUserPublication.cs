using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Data.Migrations
{
    public partial class IsLikedRemovedFromUserPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLiked",
                table: "UsersPublications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLiked",
                table: "UsersPublications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
