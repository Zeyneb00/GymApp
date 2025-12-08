using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApp.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonScheduling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonTime",
                table: "GroupLessons");

            migrationBuilder.RenameColumn(
                name: "CurrentEnrollment",
                table: "GroupLessons",
                newName: "DayOfWeek");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "LessonRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "GroupLessons",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "LessonRegistrations");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "GroupLessons");

            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "GroupLessons",
                newName: "CurrentEnrollment");

            migrationBuilder.AddColumn<DateTime>(
                name: "LessonTime",
                table: "GroupLessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
