using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class help : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareScreen_Computer_ComputerID",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.RenameColumn(
                name: "ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                newName: "ComputerId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareScreen_ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                newName: "IX_ShareScreen_ComputerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareScreen_Computer_ComputerId",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerId",
                principalSchema: "dbo",
                principalTable: "Computer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareScreen_Computer_ComputerId",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.RenameColumn(
                name: "ComputerId",
                schema: "dbo",
                table: "ShareScreen",
                newName: "ComputerID");

            migrationBuilder.RenameIndex(
                name: "IX_ShareScreen_ComputerId",
                schema: "dbo",
                table: "ShareScreen",
                newName: "IX_ShareScreen_ComputerID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareScreen_Computer_ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerID",
                principalSchema: "dbo",
                principalTable: "Computer",
                principalColumn: "Id");
        }
    }
}
