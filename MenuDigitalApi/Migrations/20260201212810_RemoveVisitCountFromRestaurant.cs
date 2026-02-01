using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigitalApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVisitCountFromRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "visitCount",
                table: "Restaurants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "visitCount",
                table: "Restaurants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
