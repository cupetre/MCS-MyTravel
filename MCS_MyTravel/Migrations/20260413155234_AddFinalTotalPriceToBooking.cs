using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCS_MyTravel.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalTotalPriceToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalTotalPrice",
                table: "Bookings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalTotalPrice",
                table: "Bookings");
        }
    }
}
