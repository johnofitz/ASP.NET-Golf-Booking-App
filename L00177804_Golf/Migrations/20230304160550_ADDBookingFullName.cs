using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L00177804Golf.Migrations
{
    /// <inheritdoc />
    public partial class ADDBookingFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Membership",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Membership");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Booking");
        }
    }
}
