using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class fixing_arrival_time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ArrivalTime",
                table: "Flights",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            // If your database system requires default values for the new column type, you may set it here.
            // migrationBuilder.Sql("UPDATE YourTableName SET ArrivalTime = CAST(GETDATE() AS TIME)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DepartureTime",
                table: "Flights",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            // If you need to handle any data conversion or default values in the down migration, do it here.

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
