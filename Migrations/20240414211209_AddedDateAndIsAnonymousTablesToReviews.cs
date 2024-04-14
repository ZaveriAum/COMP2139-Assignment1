using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateAndIsAnonymousTablesToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                schema: "Identity",
                table: "HotelReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isAnonymous",
                schema: "Identity",
                table: "HotelReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                schema: "Identity",
                table: "FlightReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isAnonymous",
                schema: "Identity",
                table: "FlightReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                schema: "Identity",
                table: "CarReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isAnonymous",
                schema: "Identity",
                table: "CarReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePosted",
                schema: "Identity",
                table: "HotelReviews");

            migrationBuilder.DropColumn(
                name: "isAnonymous",
                schema: "Identity",
                table: "HotelReviews");

            migrationBuilder.DropColumn(
                name: "DatePosted",
                schema: "Identity",
                table: "FlightReviews");

            migrationBuilder.DropColumn(
                name: "isAnonymous",
                schema: "Identity",
                table: "FlightReviews");

            migrationBuilder.DropColumn(
                name: "DatePosted",
                schema: "Identity",
                table: "CarReviews");

            migrationBuilder.DropColumn(
                name: "isAnonymous",
                schema: "Identity",
                table: "CarReviews");
        }
    }
}
