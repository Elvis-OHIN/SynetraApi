using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareScreen_Computer_ComputerID",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.AlterColumn<int>(
                name: "ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareScreen_Computer_ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerID",
                principalSchema: "dbo",
                principalTable: "Computer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareScreen_Computer_ComputerID",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.AlterColumn<int>(
                name: "ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareScreen_Computer_ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerID",
                principalSchema: "dbo",
                principalTable: "Computer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
