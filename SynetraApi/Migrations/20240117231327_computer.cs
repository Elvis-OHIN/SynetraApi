using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class computer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomsId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomsId",
                table: "Rooms",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_RoomId",
                table: "Computers",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Rooms_RoomId",
                table: "Computers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Rooms_RoomsId",
                table: "Rooms",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Rooms_RoomId",
                table: "Computers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Rooms_RoomsId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomsId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Computers_RoomId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "RoomsId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Computers");
        }
    }
}
