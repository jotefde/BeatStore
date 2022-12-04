using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore.API.Migrations
{
    public partial class TrackObjectsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "Orders");

            migrationBuilder.CreateTable(
                name: "TrackStorage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WaveFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SampleFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackObjectsId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackStorage_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackStorage_TrackId",
                table: "TrackStorage",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderDetailsId",
                table: "OrderItems",
                column: "OrderDetailsId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderDetailsId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "TrackStorage");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "OrderDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                table: "OrderItems",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
