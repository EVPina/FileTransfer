﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace FileTransfer.Migrations
{
    public partial class FilesUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesUsers",
                columns: table => new
                {
                    IdFileUser = table.Column<string>(nullable: false, maxLength: 20),
                    NameFile = table.Column<string>(nullable: false),
                    SizeFile = table.Column<string>(nullable: false, maxLength: 10),
                    DateUpload = table.Column<string>(nullable: false, maxLength: 12),
                    IdUser = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesUsers", x => x.IdFileUser);
                    table.ForeignKey(
                        name: "FK_FilesUsers_AspNetUsers_IdUserId",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesUsers_IdUserId",
                table: "FilesUsers",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesUsers");
        }
    }
}
