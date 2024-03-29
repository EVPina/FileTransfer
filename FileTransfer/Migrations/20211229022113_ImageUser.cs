﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace FileTransfer.Migrations
{
    public partial class ImageUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUser",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUser",
                table: "AspNetUsers");
        }
    }
}
