using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class idparc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParcId",
                table: "Computer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computer_ParcId",
                table: "Computer",
                column: "ParcId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computer_Parc_ParcId",
                table: "Computer",
                column: "ParcId",
                principalTable: "Parc",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computer_Parc_ParcId",
                table: "Computer");

            migrationBuilder.DropIndex(
                name: "IX_Computer_ParcId",
                table: "Computer");

            migrationBuilder.DropColumn(
                name: "ParcId",
                table: "Computer");
        }
    }
}
