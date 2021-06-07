using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shopper.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductManagers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductManagers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreKeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreKeepers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ExpiringDate = table.Column<DateTime>(nullable: true),
                    ProductStatus = table.Column<int>(nullable: false),
                    StoreKeeperId = table.Column<string>(nullable: true),
                    SupervisorId = table.Column<string>(nullable: true),
                    ProductManagerId = table.Column<string>(nullable: true),
                    ProductManagerId1 = table.Column<int>(nullable: true),
                    StoreKeeperId1 = table.Column<int>(nullable: true),
                    SupervisorId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductManagers_ProductManagerId1",
                        column: x => x.ProductManagerId1,
                        principalTable: "ProductManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_StoreKeepers_StoreKeeperId1",
                        column: x => x.StoreKeeperId1,
                        principalTable: "StoreKeepers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Supervisors_SupervisorId1",
                        column: x => x.SupervisorId1,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductManagers_UserId",
                table: "ProductManagers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductManagerId1",
                table: "Products",
                column: "ProductManagerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreKeeperId1",
                table: "Products",
                column: "StoreKeeperId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupervisorId1",
                table: "Products",
                column: "SupervisorId1");

            migrationBuilder.CreateIndex(
                name: "IX_StoreKeepers_UserId",
                table: "StoreKeepers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_UserId",
                table: "Supervisors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductManagers");

            migrationBuilder.DropTable(
                name: "StoreKeepers");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
