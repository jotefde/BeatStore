using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualBasic;

#nullable disable

namespace BeatStore.API.Migrations
{
    public partial class InitRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), Helpers.Constants.UserRole.Customer, Helpers.Constants.UserRole.Customer, Guid.NewGuid().ToString() });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), Helpers.Constants.UserRole.Admin, Helpers.Constants.UserRole.Admin, Guid.NewGuid().ToString() });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
