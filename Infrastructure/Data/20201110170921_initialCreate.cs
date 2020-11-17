using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Rent = table.Column<decimal>(type: "TEXT", nullable: false),
                    InitialDeposit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MoveInDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StayDuration = table.Column<string>(type: "TEXT", nullable: true),
                    Layout = table.Column<string>(type: "TEXT", nullable: true),
                    RoommateDescription = table.Column<string>(type: "TEXT", nullable: true),
                    DescribeNeighborhood = table.Column<string>(type: "TEXT", nullable: true),
                    NumberBedRooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberBathRooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberRoommateAllowed = table.Column<int>(type: "INTEGER", nullable: false),
                    SpaceDescription = table.Column<string>(type: "TEXT", nullable: true),
                    IsSecurityChecked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
