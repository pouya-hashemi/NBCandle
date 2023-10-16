using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedProductProperty_Products_ProductId",
                table: "FixedProductProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixedProductProperty",
                table: "FixedProductProperty");

            migrationBuilder.RenameTable(
                name: "FixedProductProperty",
                newName: "FixedProductProperties");

            migrationBuilder.RenameIndex(
                name: "IX_FixedProductProperty_ProductId",
                table: "FixedProductProperties",
                newName: "IX_FixedProductProperties_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixedProductProperties",
                table: "FixedProductProperties",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedProductProperties_Products_ProductId",
                table: "FixedProductProperties",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedProductProperties_Products_ProductId",
                table: "FixedProductProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixedProductProperties",
                table: "FixedProductProperties");

            migrationBuilder.RenameTable(
                name: "FixedProductProperties",
                newName: "FixedProductProperty");

            migrationBuilder.RenameIndex(
                name: "IX_FixedProductProperties_ProductId",
                table: "FixedProductProperty",
                newName: "IX_FixedProductProperty_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixedProductProperty",
                table: "FixedProductProperty",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedProductProperty_Products_ProductId",
                table: "FixedProductProperty",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
