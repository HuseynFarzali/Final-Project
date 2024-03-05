using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.DAL.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseStudents");

            migrationBuilder.DropColumn(
                name: "ConnectedEntityId",
                table: "AppUsers");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "AppUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserId",
                table: "Students",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AppUsers_AppUserId",
                table: "Students",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AppUsers_AppUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AppUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "AppUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ConnectedEntityId",
                table: "AppUsers",
                type: "integer",
                nullable: true);
        }
    }
}
