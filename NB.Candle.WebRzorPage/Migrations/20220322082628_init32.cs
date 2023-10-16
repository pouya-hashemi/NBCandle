using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingMethodId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PaymentImageUrl",
                table: "CartInfos");

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDaysToDeliver",
                table: "ShippingMethods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "OrderNumber",
                table: "OrderMasters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedDaysToDeliver",
                table: "ShippingMethods");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "OrderMasters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingMethodId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PaymentImageUrl",
                table: "CartInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
