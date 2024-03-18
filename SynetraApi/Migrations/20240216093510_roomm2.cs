using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class roomm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Parc_ParcsId",
                schema: "dbo",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "ParcsId",
                schema: "dbo",
                table: "Room",
                newName: "ParcId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_ParcsId",
                schema: "dbo",
                table: "Room",
                newName: "IX_Room_ParcId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Parc_ParcId",
                schema: "dbo",
                table: "Room",
                column: "ParcId",
                principalSchema: "dbo",
                principalTable: "Parc",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Parc_ParcId",
                schema: "dbo",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "ParcId",
                schema: "dbo",
                table: "Room",
                newName: "ParcsId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_ParcId",
                schema: "dbo",
                table: "Room",
                newName: "IX_Room_ParcsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Parc_ParcsId",
                schema: "dbo",
                table: "Room",
                column: "ParcsId",
                principalSchema: "dbo",
                principalTable: "Parc",
                principalColumn: "Id");
        }
    }
}
