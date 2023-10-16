using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Carts_ColorId",
                table: "Carts",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Colors_ColorId",
                table: "Carts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Colors_ColorId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ColorId",
                table: "Carts");
        }
    }
}
