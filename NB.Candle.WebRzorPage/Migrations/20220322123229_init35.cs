using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "OrderMasters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "OrderMasters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "OrderMasters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscountAmount",
                table: "OrderMasters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalProductAmount",
                table: "OrderMasters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "TotalDiscountAmount",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "TotalProductAmount",
                table: "OrderMasters");
        }
    }
}
