using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProgressPropsInEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedDays",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentDay",
                table: "Enrollments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaused",
                table: "Enrollments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivityAt",
                table: "Enrollments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                table: "Enrollments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedDays",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CurrentDay",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "IsPaused",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "LastActivityAt",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                table: "Enrollments");
        }
    }
}
