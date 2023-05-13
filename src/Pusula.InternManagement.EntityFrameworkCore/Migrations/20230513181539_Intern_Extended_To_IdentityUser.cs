using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pusula.InternManagement.Migrations
{
    /// <inheritdoc />
    public partial class InternExtendedToIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppInterns");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AppInterns");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInterns_AbpUsers_Id",
                table: "AppInterns",
                column: "Id",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInterns_AbpUsers_Id",
                table: "AppInterns");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppInterns",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppInterns",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppInterns",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppInterns",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppInterns",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppInterns",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppInterns",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppInterns",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppInterns",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppInterns",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppInterns",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AppInterns",
                type: "text",
                nullable: true);
        }
    }
}
