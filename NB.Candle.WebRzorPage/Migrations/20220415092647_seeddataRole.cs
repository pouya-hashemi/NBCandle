using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class seeddataRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "798e8fd6-57dc-4022-ad49-9a6fce2163f2");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "9ac773d4-b650-472c-b05d-537ede57a9dd", "79031766-b526-4104-9e19-594a63026ee5", "Vendor", "Vendor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ac773d4-b650-472c-b05d-537ede57a9dd");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserAddress", "UserName" },
                values: new object[] { "798e8fd6-57dc-4022-ad49-9a6fce2163f2", 0, "5426dbcf-2cb4-4249-9875-0b1af80711e6", "User", null, false, "Vendor", "Vendor", false, null, null, null, "AQAAAAEAACcQAAAAEIfbyEakReHFgytaFZa/GDwYJ6guRiwOf87lksUNtqMMskUNeP2DnUSA+Ds++pKBFA==", null, false, null, "f5efb516-fb5b-4b06-8cc6-13e050e52b91", false, null, "vendor" });
        }
    }
}
