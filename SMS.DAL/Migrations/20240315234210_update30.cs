using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.DAL.Migrations
{
    public partial class update30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "AppUserIds",
                table: "Chats",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "AppUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ChatId",
                table: "AppUsers",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Chats_ChatId",
                table: "AppUsers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Chats_ChatId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ChatId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AppUserIds",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "AppUsers");
        }
    }
}
