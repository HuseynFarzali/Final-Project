using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.DAL.Migrations
{
    public partial class update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModule");

            migrationBuilder.DropTable(
                name: "LessonModule");

            migrationBuilder.DropColumn(
                name: "LessonIds",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "UsedInCourseIds",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "UsedInModuleIds",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ModuleIds",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseModules",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    ModuleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModules", x => new { x.CourseId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_CourseModules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonModules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonModules", x => new { x.LessonId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_LessonModules_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModules_ModuleId",
                table: "CourseModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonModules_ModuleId",
                table: "LessonModules",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModules");

            migrationBuilder.DropTable(
                name: "LessonModules");

            migrationBuilder.AddColumn<int[]>(
                name: "LessonIds",
                table: "Modules",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "UsedInCourseIds",
                table: "Modules",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "UsedInModuleIds",
                table: "Lessons",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "ModuleIds",
                table: "Courses",
                type: "integer[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseModule",
                columns: table => new
                {
                    ModulesId = table.Column<int>(type: "integer", nullable: false),
                    UsedInCoursesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModule", x => new { x.ModulesId, x.UsedInCoursesId });
                    table.ForeignKey(
                        name: "FK_CourseModule_Courses_UsedInCoursesId",
                        column: x => x.UsedInCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseModule_Modules_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonModule",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "integer", nullable: false),
                    UsedInModulesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonModule", x => new { x.LessonsId, x.UsedInModulesId });
                    table.ForeignKey(
                        name: "FK_LessonModule_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonModule_Modules_UsedInModulesId",
                        column: x => x.UsedInModulesId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModule_UsedInCoursesId",
                table: "CourseModule",
                column: "UsedInCoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonModule_UsedInModulesId",
                table: "LessonModule",
                column: "UsedInModulesId");
        }
    }
}
