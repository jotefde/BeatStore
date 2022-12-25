using System;
using System.Reflection;
using BeatStore.API.Helpers;
using BeatStore.API.Helpers.Constants;
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

            object[,] tracks = new object[10,7];
            for (int i = 0; i < 10; i++)
            {
                tracks[i, 0] = Guid.NewGuid().ToString();
                var name = $"Random name #{i + 1}";
                tracks[i, 1] = name;
                tracks[i, 2] = (float)25.00;
                tracks[i, 3] = "Here is some description";
                tracks[i, 4] = Slugify.Generate(name);
                tracks[i, 5] = DateTime.Now;
                tracks[i, 6] = DateTime.Now;
            }
            migrationBuilder.InsertData("Tracks", new string[] { "Id", "Name", "Price", "Description", "Slug", "ModifiedTime", "CreatedTime" }, tracks);

            object[,] stock = new object[10,8];
            for (int i = 0; i < 10; i++)
            {
                stock[i, 0] = Guid.NewGuid().ToString();
                stock[i, 1] = tracks[i,0];
                stock[i, 2] = 0;
                stock[i, 3] = DateTime.Now;
                stock[i, 4] = true;
                stock[i, 5] = true;
                stock[i, 6] = DateTime.Now;
                stock[i, 7] = DateTime.Now;
            }
            migrationBuilder.InsertData("Stock", new string[] { "Id", "TrackId", "Amount", "PublishTime", "IsUnlimited", "IsPublished", "ModifiedTime", "CreatedTime" }, stock);

            object[] adminUser = new object[15]
            {
                "49fae637-95a6-4302-8268-f88b0322de21",
                "admin@mail.com",
                "ADMIN@MAIL.COM",
                null, null, false,
                "AQAAAAEAACcQAAAAEOYt/VrfPXJyBta6W5atTCQ0/G9o3uGnYkJyKPfrWY5LKFedehifHouTyUW1iY2ocg==",
                "5ON2VGJ2JKYWPKVCDUID5IZ5RXPMI7GU",
                "bf1fed1c-8622-41b9-a219-b934c7a23fa9",
                null, false, false, null, true, 0
            };
            migrationBuilder.InsertData("AspNetUsers", new string[] {
                "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" }, adminUser);


            var adminRole = "";
            var userRoles = new List<string>();
            foreach (var prop in typeof(UserRole).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var val = (string)prop.GetValue(null);
                if(!val.Equals("UNAUTHORIZED"))
                    userRoles.Add(val);
            }
            object[,] roles = new object[userRoles.Count, 4];
            for (int i = 0; i < userRoles.Count; i++)
            {
                roles[i, 0] = Guid.NewGuid().ToString();
                if (userRoles[i].Equals("ADMINISTRATOR"))
                    adminRole = Convert.ToString(roles[i, 0]);
                roles[i, 1] = userRoles[i];
                roles[i, 2] = userRoles[i];
                roles[i, 3] = Guid.NewGuid().ToString();
            }
            migrationBuilder.InsertData("AspNetRoles", new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }, roles);


            object[] adminUserRole = new object[2]
            {
                "49fae637-95a6-4302-8268-f88b0322de21",
                adminRole,
            };
            migrationBuilder.InsertData("AspNetUserRoles", new string[] { "UserId", "RoleId" }, adminUserRole);
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
