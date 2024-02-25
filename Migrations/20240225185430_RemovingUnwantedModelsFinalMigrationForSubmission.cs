using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUnwantedModelsFinalMigrationForSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoCars");
            migrationBuilder.DropTable(
                name: "PhotoRooms");
            migrationBuilder.DropTable(
                name: "PhotoHotels");
            migrationBuilder.DropTable(
                name: "ReviewCars");
            migrationBuilder.DropTable(
                name: "ReviewRooms");
            migrationBuilder.DropTable(
                name: "ReviewHotels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
