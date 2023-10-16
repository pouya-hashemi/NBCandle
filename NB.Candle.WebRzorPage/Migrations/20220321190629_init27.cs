using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ShippingMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ShippingMethods");
        }
    }
}
