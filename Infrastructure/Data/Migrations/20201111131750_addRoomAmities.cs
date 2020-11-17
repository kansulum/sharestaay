using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class addRoomAmities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_Rooms_RoomId",
                table: "Amenity");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Rooms_RoomId",
                table: "Rule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rule",
                table: "Rule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amenity",
                table: "Amenity");

            migrationBuilder.RenameTable(
                name: "Rule",
                newName: "Rules");

            migrationBuilder.RenameTable(
                name: "Amenity",
                newName: "Amenities");

            migrationBuilder.RenameIndex(
                name: "IX_Rule_RoomId",
                table: "Rules",
                newName: "IX_Rules_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Amenity_RoomId",
                table: "Amenities",
                newName: "IX_Amenities_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rules",
                table: "Rules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amenities",
                table: "Amenities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoomAmenities",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenities", x => new { x.RoomId, x.AmenitiesId });
                    table.ForeignKey(
                        name: "FK_RoomAmenities_Amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomAmenities_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenitiesId",
                table: "RoomAmenities",
                column: "AmenitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Rooms_RoomId",
                table: "Amenities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Rooms_RoomId",
                table: "Rules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Rooms_RoomId",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Rooms_RoomId",
                table: "Rules");

            migrationBuilder.DropTable(
                name: "RoomAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rules",
                table: "Rules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amenities",
                table: "Amenities");

            migrationBuilder.RenameTable(
                name: "Rules",
                newName: "Rule");

            migrationBuilder.RenameTable(
                name: "Amenities",
                newName: "Amenity");

            migrationBuilder.RenameIndex(
                name: "IX_Rules_RoomId",
                table: "Rule",
                newName: "IX_Rule_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Amenities_RoomId",
                table: "Amenity",
                newName: "IX_Amenity_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rule",
                table: "Rule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amenity",
                table: "Amenity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenity_Rooms_RoomId",
                table: "Amenity",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Rooms_RoomId",
                table: "Rule",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
