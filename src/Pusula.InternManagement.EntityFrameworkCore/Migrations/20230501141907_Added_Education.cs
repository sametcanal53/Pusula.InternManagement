using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.InternManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEducations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UniversityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UniversityDepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    GradePointAverage = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    table.PrimaryKey("PK_AppEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppEducations_AppInterns_InternId",
                        column: x => x.InternId,
                        principalTable: "AppInterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppEducations_AppUniversities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "AppUniversities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppEducations_AppUniversityDepartments_UniversityDepartment~",
                        column: x => x.UniversityDepartmentId,
                        principalTable: "AppUniversityDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppEducations_InternId",
                table: "AppEducations",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEducations_UniversityDepartmentId",
                table: "AppEducations",
                column: "UniversityDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEducations_UniversityId",
                table: "AppEducations",
                column: "UniversityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEducations");
        }
    }
}
