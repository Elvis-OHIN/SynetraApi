using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class parc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParcId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ParcId",
                table: "User",
                column: "ParcId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Parc_ParcId",
                table: "User",
                column: "ParcId",
                principalTable: "Parc",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Parc_ParcId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ParcId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ParcId",
                table: "User");
        }
    }
}
