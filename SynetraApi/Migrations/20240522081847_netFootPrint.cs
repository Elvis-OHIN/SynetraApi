using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class netFootPrint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarteMere",
                table: "NetworkInfo",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "NetworkInfo",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NetworkInfo",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnable",
                table: "NetworkInfo",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "NetworkInfo",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarteMere",
                table: "NetworkInfo");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "NetworkInfo");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NetworkInfo");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "NetworkInfo");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "NetworkInfo");
        }
    }
}
