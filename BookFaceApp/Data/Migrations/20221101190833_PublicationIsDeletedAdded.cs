﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookFaceApp.Data.Migrations
{
    public partial class PublicationIsDeletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Publications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Publications");
        }
    }
}