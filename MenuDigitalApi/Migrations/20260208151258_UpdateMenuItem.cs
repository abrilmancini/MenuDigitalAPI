using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigitalApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "MenuItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "MenuItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHappyHourEnabled",
                table: "MenuItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "IsHappyHourEnabled",
                table: "MenuItems");
        }
    }
}
