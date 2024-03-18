using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class room5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Parc_ParcsId",
                schema: "dbo",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "ParcsId",
                schema: "dbo",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Parc_ParcsId",
                schema: "dbo",
                table: "Room",
                column: "ParcsId",
                principalSchema: "dbo",
                principalTable: "Parc",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Parc_ParcsId",
                schema: "dbo",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "ParcsId",
                schema: "dbo",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Parc_ParcsId",
                schema: "dbo",
                table: "Room",
                column: "ParcsId",
                principalSchema: "dbo",
                principalTable: "Parc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
