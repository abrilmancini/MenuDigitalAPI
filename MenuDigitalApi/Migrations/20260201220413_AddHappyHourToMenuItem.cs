using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigitalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHappyHourToMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HappyHourEnd",
                table: "MenuItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HappyHourPrice",
                table: "MenuItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HappyHourStart",
                table: "MenuItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HappyHourEnd",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "HappyHourPrice",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "HappyHourStart",
                table: "MenuItems");
        }
    }
}
