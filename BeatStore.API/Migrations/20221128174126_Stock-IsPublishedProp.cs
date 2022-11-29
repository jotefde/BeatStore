using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore.API.Migrations
{
    public partial class StockIsPublishedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_OrderDetails_OrderDetailsId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Tracks_TrackId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Tracks_TrackId",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_TrackId",
                table: "OrderItems",
                newName: "IX_OrderItems_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderDetailsId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderDetailsId");

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "Stock",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Stock",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "TrackId",
                table: "OrderItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                table: "OrderItems",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Tracks_TrackId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Tracks_TrackId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_TrackId",
                table: "OrderItem",
                newName: "IX_OrderItem_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderDetailsId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderDetailsId");

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
                table: "OrderItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_OrderDetails_OrderDetailsId",
                table: "OrderItem",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Tracks_TrackId",
                table: "OrderItem",
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
    }
}
