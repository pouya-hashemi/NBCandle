using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init59 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPersentage",
                table: "OrderDiscounts",
                newName: "DiscountPercentage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "OrderDiscounts",
                newName: "DiscountPersentage");
        }
    }
}
