using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore.API.Migrations
{
    public partial class OrderAccessEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackObjectsId",
                table: "TrackStorage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackAccessId",
                table: "TrackStorage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderAccesses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAccesses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAccesses_OrderId",
                table: "OrderAccesses",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAccesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackAccessId",
                table: "TrackStorage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackObjectsId",
                table: "TrackStorage",
                column: "Id");
        }
    }
}
