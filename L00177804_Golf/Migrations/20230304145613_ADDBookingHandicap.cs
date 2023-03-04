using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L00177804Golf.Migrations
{
    /// <inheritdoc />
    public partial class ADDBookingHandicap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Handicap",
                table: "Booking",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Handicap",
                table: "Booking");
        }
    }
}
