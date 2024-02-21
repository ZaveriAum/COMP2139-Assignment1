﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class addDateInFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
            name: "ArrivalDate",
            table: "Flights",
            type: "date",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
