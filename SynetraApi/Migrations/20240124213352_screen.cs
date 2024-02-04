using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class screen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShareScreen",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerID = table.Column<int>(type: "int", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareScreen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareScreen_Computer_ComputerID",
                        column: x => x.ComputerID,
                        principalSchema: "dbo",
                        principalTable: "Computer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShareScreen_ComputerID",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareScreen",
                schema: "dbo");
        }
    }
}
