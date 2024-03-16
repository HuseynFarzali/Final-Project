using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SMS.DAL.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Teachers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundImageId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundImageId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageName = table.Column<string>(type: "text", nullable: true),
                    ImageDescription = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ImageId",
                table: "Teachers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ImageId",
                table: "Students",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_BackgroundImageId",
                table: "Courses",
                column: "BackgroundImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BackgroundImageId",
                table: "Categories",
                column: "BackgroundImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Images_BackgroundImageId",
                table: "Categories",
                column: "BackgroundImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_BackgroundImageId",
                table: "Courses",
                column: "BackgroundImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Images_ImageId",
                table: "Students",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Images_ImageId",
                table: "Teachers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Images_BackgroundImageId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_BackgroundImageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Images_ImageId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Images_ImageId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ImageId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_ImageId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Courses_BackgroundImageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BackgroundImageId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BackgroundImageId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "BackgroundImageId",
                table: "Categories");
        }
    }
}
