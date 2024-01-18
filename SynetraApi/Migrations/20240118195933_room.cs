using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class room : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Room_RoomId",
                schema: "dbo",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomId",
                schema: "dbo",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "dbo",
                table: "Room");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                schema: "dbo",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomId",
                schema: "dbo",
                table: "Room",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Room_RoomId",
                schema: "dbo",
                table: "Room",
                column: "RoomId",
                principalSchema: "dbo",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
