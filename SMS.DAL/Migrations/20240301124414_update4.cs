using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.DAL.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Bound",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bound",
                table: "Students");
        }
    }
}
