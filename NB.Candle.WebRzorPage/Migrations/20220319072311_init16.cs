using Microsoft.EntityFrameworkCore.Migrations;

namespace NB.Candle.WebRzorPage.Migrations
{
    public partial class init16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOtps_AspNetUsers_UserId",
                table: "UserOtps");

            migrationBuilder.DropIndex(
                name: "IX_UserOtps_UserId",
                table: "UserOtps");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserOtps");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserOtps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserOtps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserOtps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswortReEnter",
                table: "UserOtps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserOtps",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserOtps");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserOtps");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserOtps");

            migrationBuilder.DropColumn(
                name: "PasswortReEnter",
                table: "UserOtps");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserOtps");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserOtps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOtps_UserId",
                table: "UserOtps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOtps_AspNetUsers_UserId",
                table: "UserOtps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
