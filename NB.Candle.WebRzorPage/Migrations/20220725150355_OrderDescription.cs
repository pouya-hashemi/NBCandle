using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class OrderDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "9ac773d4-b650-472c-b05d-537ede57a9dd");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderMasters",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "f252a5d5-e36d-4f93-86fb-bdf9f3b950ba", "31656398-e020-4084-a5d4-2c37f1d75786", "Vendor", "Vendor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f252a5d5-e36d-4f93-86fb-bdf9f3b950ba");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderMasters");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ac773d4-b650-472c-b05d-537ede57a9dd", "79031766-b526-4104-9e19-594a63026ee5", "Vendor", "Vendor" });
        }
    }
}
