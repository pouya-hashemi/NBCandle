using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderMasters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShippingMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostTrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderMasters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderMasters_ShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderMasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductProperty_Height = table.Column<float>(type: "real", nullable: true),
                    ProductProperty_Diameter = table.Column<float>(type: "real", nullable: true),
                    ProductProperty_Width = table.Column<float>(type: "real", nullable: true),
                    ProductProperty_Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderMasters_OrderMasterId",
                        column: x => x.OrderMasterId,
                        principalTable: "OrderMasters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ColorId",
                table: "OrderItems",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderMasterId",
                table: "OrderItems",
                column: "OrderMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasters_ShippingMethodId",
                table: "OrderMasters",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasters_UserId",
                table: "OrderMasters",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderMasters");
        }
    }
}
