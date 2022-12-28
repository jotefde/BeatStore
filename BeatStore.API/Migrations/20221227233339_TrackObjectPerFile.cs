using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore.API.Migrations
{
    public partial class TrackObjectPerFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackStorage");

            migrationBuilder.CreateTable(
                name: "TrackObjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectType = table.Column<int>(type: "int", nullable: false),
                    MIME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackAccessId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackObjects_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackObjects_TrackId",
                table: "TrackObjects",
                column: "TrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackObjects");

            migrationBuilder.CreateTable(
                name: "TrackStorage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SampleFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackoutFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaveFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackAccessId", x => x.Id);
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
        }
    }
}
