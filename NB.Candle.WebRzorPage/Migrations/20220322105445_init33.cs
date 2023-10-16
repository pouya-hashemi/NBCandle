using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAddresses");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderMasters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddreess",
                table: "OrderMasters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingFullName",
                table: "OrderMasters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingPhoneNumber",
                table: "OrderMasters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "ShippingAddreess",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "ShippingFullName",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "ShippingPhoneNumber",
                table: "OrderMasters");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserAddresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
