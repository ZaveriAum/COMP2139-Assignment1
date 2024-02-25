using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class AddNumPassengerInFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.DropTable(
            //        name: "PhotoCars");

            //    migrationBuilder.DropTable(
            //        name: "PhotoHotels");

            //    migrationBuilder.DropTable(
            //        name: "PhotoRooms");

            //    migrationBuilder.DropTable(
            //        name: "ReviewCars");

            //    migrationBuilder.DropTable(
            //        name: "ReviewHotels");

            //    migrationBuilder.DropTable(
            //        name: "ReviewRooms");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPassenger",
                table: "FlightBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPassenger",
                table: "FlightBookings");

            migrationBuilder.CreateTable(
                name: "PhotoCars",
                columns: table => new
                {
                    PhotoCarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoCars", x => x.PhotoCarId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoHotels",
                columns: table => new
                {
                    PhotoHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoHotels", x => x.PhotoHotelId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoRooms",
                columns: table => new
                {
                    PhotoRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoRooms", x => x.PhotoRoomId);
                });

            migrationBuilder.CreateTable(
                name: "ReviewCars",
                columns: table => new
                {
                    ReviewCarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewCars", x => x.ReviewCarId);
                });

            migrationBuilder.CreateTable(
                name: "ReviewHotels",
                columns: table => new
                {
                    ReviewHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewHotels", x => x.ReviewHotelId);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRooms",
                columns: table => new
                {
                    ReviewRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Review = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRooms", x => x.ReviewRoomId);
                });
        }
    }
}
