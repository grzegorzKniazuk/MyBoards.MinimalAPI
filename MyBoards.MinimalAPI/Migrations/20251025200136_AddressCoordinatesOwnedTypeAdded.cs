using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddressCoordinatesOwnedTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coordinates_Latitude",
                table: "UserAddresses",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coordinates_Longitude",
                table: "UserAddresses",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates_Latitude",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "Coordinates_Longitude",
                table: "UserAddresses");
        }
    }
}
