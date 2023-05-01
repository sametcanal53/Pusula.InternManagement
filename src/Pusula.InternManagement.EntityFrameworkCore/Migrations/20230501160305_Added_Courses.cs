using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.InternManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInternProjects");

            migrationBuilder.CreateTable(
                name: "AppCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectInterns",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectInterns", x => new { x.ProjectId, x.InternId });
                    table.ForeignKey(
                        name: "FK_AppProjectInterns_AppInterns_InternId",
                        column: x => x.InternId,
                        principalTable: "AppInterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectInterns_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCourseInstructors",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCourseInstructors", x => new { x.CourseId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_AppCourseInstructors_AppCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "AppCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCourseInstructors_AppInstructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AppInstructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCourseInterns",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCourseInterns", x => new { x.CourseId, x.InternId });
                    table.ForeignKey(
                        name: "FK_AppCourseInterns_AppCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "AppCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCourseInterns_AppInterns_InternId",
                        column: x => x.InternId,
                        principalTable: "AppInterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCourseInstructors_CourseId_InstructorId",
                table: "AppCourseInstructors",
                columns: new[] { "CourseId", "InstructorId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppCourseInstructors_InstructorId",
                table: "AppCourseInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCourseInterns_CourseId_InternId",
                table: "AppCourseInterns",
                columns: new[] { "CourseId", "InternId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppCourseInterns_InternId",
                table: "AppCourseInterns",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectInterns_InternId",
                table: "AppProjectInterns",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectInterns_ProjectId_InternId",
                table: "AppProjectInterns",
                columns: new[] { "ProjectId", "InternId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCourseInstructors");

            migrationBuilder.DropTable(
                name: "AppCourseInterns");

            migrationBuilder.DropTable(
                name: "AppProjectInterns");

            migrationBuilder.DropTable(
                name: "AppCourses");

            migrationBuilder.CreateTable(
                name: "AppInternProjects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInternProjects", x => new { x.ProjectId, x.InternId });
                    table.ForeignKey(
                        name: "FK_AppInternProjects_AppInterns_InternId",
                        column: x => x.InternId,
                        principalTable: "AppInterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppInternProjects_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInternProjects_InternId",
                table: "AppInternProjects",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInternProjects_ProjectId_InternId",
                table: "AppInternProjects",
                columns: new[] { "ProjectId", "InternId" });
        }
    }
}
