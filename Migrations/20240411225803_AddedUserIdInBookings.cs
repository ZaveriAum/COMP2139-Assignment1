using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdInBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "RoomBookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "FlightBookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "CarBookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_UserId",
                schema: "Identity",
                table: "RoomBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_UserId",
                schema: "Identity",
                table: "FlightBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBookings_UserId",
                schema: "Identity",
                table: "CarBookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBookings_User_UserId",
                schema: "Identity",
                table: "CarBookings",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBookings_User_UserId",
                schema: "Identity",
                table: "FlightBookings",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_User_UserId",
                schema: "Identity",
                table: "RoomBookings",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBookings_User_UserId",
                schema: "Identity",
                table: "CarBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightBookings_User_UserId",
                schema: "Identity",
                table: "FlightBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_User_UserId",
                schema: "Identity",
                table: "RoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookings_UserId",
                schema: "Identity",
                table: "RoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_FlightBookings_UserId",
                schema: "Identity",
                table: "FlightBookings");

            migrationBuilder.DropIndex(
                name: "IX_CarBookings_UserId",
                schema: "Identity",
                table: "CarBookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "FlightBookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "CarBookings");
        }
    }
}
