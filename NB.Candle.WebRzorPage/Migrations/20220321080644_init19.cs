using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Products_ProductId",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Colors_ProductId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Colors");

            migrationBuilder.CreateTable(
                name: "ColorProduct",
                columns: table => new
                {
                    ColorsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorProduct", x => new { x.ColorsID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_ColorProduct_Colors_ColorsID",
                        column: x => x.ColorsID,
                        principalTable: "Colors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorProduct_ProductsID",
                table: "ColorProduct",
                column: "ProductsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Colors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ProductId",
                table: "Colors",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Products_ProductId",
                table: "Colors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
