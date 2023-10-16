using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayOnHomePage",
                table: "OrderDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "OrderDiscounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayOnHomePage",
                table: "NormalDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "NormalDiscounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOnHomePage",
                table: "OrderDiscounts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "OrderDiscounts");

            migrationBuilder.DropColumn(
                name: "DisplayOnHomePage",
                table: "NormalDiscounts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "NormalDiscounts");
        }
    }
}
