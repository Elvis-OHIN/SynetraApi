using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class footPrint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarteMere",
                schema: "dbo",
                table: "NetworkInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FootPrint",
                schema: "dbo",
                table: "Computer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarteMere",
                schema: "dbo",
                table: "NetworkInfo");

            migrationBuilder.DropColumn(
                name: "FootPrint",
                schema: "dbo",
                table: "Computer");
        }
    }
}
