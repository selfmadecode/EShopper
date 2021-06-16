using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shopper.Data.Migrations
{
    public partial class AddCommentToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Products");
        }
    }
}
