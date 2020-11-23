using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class rules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Rooms_RoomId",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_Rules_RoomId",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Rules");

            migrationBuilder.CreateTable(
                name: "RoomRule",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRule", x => new { x.RoomId, x.RuleId });
                    table.ForeignKey(
                        name: "FK_RoomRule_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomRule_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomRule_RuleId",
                table: "RoomRule",
                column: "RuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomRule");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Rules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rules_RoomId",
                table: "Rules",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Rooms_RoomId",
                table: "Rules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
