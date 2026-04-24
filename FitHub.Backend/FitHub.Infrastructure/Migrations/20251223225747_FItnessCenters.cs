using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FItnessCenters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "UsersFitnessPlanTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsersFitnessPlanTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedAtUtc",
                table: "UsersFitnessPlanTypes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsersFitnessPlanTypes",
                newName: "UsersFitnessPlanTypesID");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Workouts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "UsersFitnessPlanTypesID",
                table: "UsersFitnessPlanTypes",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "UsersFitnessPlanTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsersFitnessPlanTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtUtc",
                table: "UsersFitnessPlanTypes",
                type: "datetime2",
                nullable: true);
        }
    }
}
