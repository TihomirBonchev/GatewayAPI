using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusalaApi.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateway",
                columns: table => new
                {
                    SerialNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IPAddress = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateway", x => x.SerialNumber);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vendor = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 7, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_Gateway_SerialNumber",
                        column: x => x.SerialNumber,
                        principalTable: "Gateway",
                        principalColumn: "SerialNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_SerialNumber",
                table: "Device",
                column: "SerialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Gateway");
        }
    }
}
