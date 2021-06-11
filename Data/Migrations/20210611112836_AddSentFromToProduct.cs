using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shopper.Data.Migrations
{
    public partial class AddSentFromToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentBy",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentBy",
                table: "Products");
        }
    }
}
