using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore.API.Migrations
{
    public partial class ForeignKeysImplemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Tracks_TrackId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Tracks_TrackId",
                table: "Stock");

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "Stock",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "OrderItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Tracks_TrackId",
                table: "OrderItems",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Tracks_TrackId",
                table: "Stock",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Tracks_TrackId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Tracks_TrackId",
                table: "Stock");

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "Stock",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "OrderItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Tracks_TrackId",
                table: "OrderItems",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Tracks_TrackId",
                table: "Stock",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }
    }
}
