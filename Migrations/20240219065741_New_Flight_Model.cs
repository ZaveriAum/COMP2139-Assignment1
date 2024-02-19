using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class New_Flight_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessClassSeats",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "EconomySeats",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FirstClass",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "PremiumSeats",
                table: "Flights",
                newName: "Seats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Seats",
                table: "Flights",
                newName: "PremiumSeats");

            migrationBuilder.AddColumn<string>(
                name: "BusinessClassSeats",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EconomySeats",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstClass",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
