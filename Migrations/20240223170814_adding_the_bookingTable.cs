using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class adding_the_bookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "CarBookings",
             columns: table => new
             {
                 Id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                 BookedStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                 BookedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                 CarId = table.Column<int>(type: "int", nullable: false),
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_CarBookings", x => x.Id);
                 table.ForeignKey(
                     name: "FK_CarBookings_Cars_CarId",
                     column: x => x.CarId,
                     principalTable: "Cars",
                     principalColumn: "CarId",
                     onDelete: ReferentialAction.Cascade);
             });

            migrationBuilder.CreateIndex(
            name: "IX_CarBookings_CarId",
            table: "CarBookings",
            column: "CarId");


            migrationBuilder.AlterColumn<double>(
                    name: "Rating",
                    table: "Cars",
                    type: "float",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpLocation",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.DropTable(
           name: "CarBookings");
        }
    }
}
