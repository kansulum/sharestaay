using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AgeBracket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeBrackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoommateAgeBracket",
                columns: table => new
                {
                    PreferedAgeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    AgeBracketId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoommateAgeBracket", x => new { x.PreferedAgeId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_RoommateAgeBracket_AgeBrackets_AgeBracketId",
                        column: x => x.AgeBracketId,
                        principalTable: "AgeBrackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoommateAgeBracket_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoommateAgeBracket_AgeBracketId",
                table: "RoommateAgeBracket",
                column: "AgeBracketId");

            migrationBuilder.CreateIndex(
                name: "IX_RoommateAgeBracket_RoomId",
                table: "RoommateAgeBracket",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoommateAgeBracket");

            migrationBuilder.DropTable(
                name: "AgeBrackets");
        }
    }
}
