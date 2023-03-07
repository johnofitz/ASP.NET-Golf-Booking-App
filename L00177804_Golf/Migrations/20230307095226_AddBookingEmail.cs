﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L00177804Golf.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Booking");
        }
    }
}
