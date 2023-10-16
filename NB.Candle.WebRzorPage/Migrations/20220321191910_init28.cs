using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartInfos_ShippingMethodId",
                table: "CartInfos",
                column: "ShippingMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartInfos_ShippingMethods_ShippingMethodId",
                table: "CartInfos",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartInfos_ShippingMethods_ShippingMethodId",
                table: "CartInfos");

            migrationBuilder.DropIndex(
                name: "IX_CartInfos_ShippingMethodId",
                table: "CartInfos");
        }
    }
}
