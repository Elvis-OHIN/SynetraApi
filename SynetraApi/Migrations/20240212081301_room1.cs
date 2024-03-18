using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynetraApi.Migrations
{
    /// <inheritdoc />
    public partial class room1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareScreen_Computer_ComputerId",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.DropIndex(
                name: "IX_ShareScreen_ComputerId",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.DropColumn(
                name: "ComputerId",
                schema: "dbo",
                table: "ShareScreen");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "NetworkInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubnetMask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultGateway = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNServers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetworkInfo_Computer_ComputerId",
                        column: x => x.ComputerId,
                        principalSchema: "dbo",
                        principalTable: "Computer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NetworkInfo_ComputerId",
                schema: "dbo",
                table: "NetworkInfo",
                column: "ComputerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NetworkInfo",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "ComputerId",
                schema: "dbo",
                table: "ShareScreen",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                schema: "dbo",
                table: "Room",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ShareScreen_ComputerId",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareScreen_Computer_ComputerId",
                schema: "dbo",
                table: "ShareScreen",
                column: "ComputerId",
                principalSchema: "dbo",
                principalTable: "Computer",
                principalColumn: "Id");
        }
    }
}
