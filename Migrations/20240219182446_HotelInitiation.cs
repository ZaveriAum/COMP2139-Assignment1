﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class HotelInitiation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HotelId",
                table: "Hotels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    PlateNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    RentalCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.PlateNumber);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Highlights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    photoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPlateNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HotelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    photoLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.photoId);
                    table.ForeignKey(
                        name: "FK_Photos_Cars_CarPlateNumber",
                        column: x => x.CarPlateNumber,
                        principalTable: "Cars",
                        principalColumn: "PlateNumber");
                    table.ForeignKey(
                        name: "FK_Photos_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_Photos_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPlateNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HotelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Reviews_Cars_CarPlateNumber",
                        column: x => x.CarPlateNumber,
                        principalTable: "Cars",
                        principalColumn: "PlateNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_Reviews_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CarPlateNumber",
                table: "Photos",
                column: "CarPlateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_HotelId",
                table: "Photos",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RoomId",
                table: "Photos",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CarPlateNumber",
                table: "Reviews",
                column: "CarPlateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews",
                column: "RoomId");
        }
    }
}
