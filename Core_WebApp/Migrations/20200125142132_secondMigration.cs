using Microsoft.EntityFrameworkCore.Migrations;

namespace Core_WebApp.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryRowId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryRowId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryRowId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryRowId1",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryRowId1",
                table: "Products",
                column: "CategoryRowId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryRowId1",
                table: "Products",
                column: "CategoryRowId1",
                principalTable: "Categories",
                principalColumn: "CategoryRowId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryRowId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryRowId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryRowId1",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryRowId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryRowId",
                table: "Products",
                column: "CategoryRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryRowId",
                table: "Products",
                column: "CategoryRowId",
                principalTable: "Categories",
                principalColumn: "CategoryRowId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
