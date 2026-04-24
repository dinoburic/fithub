using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedOnWishListItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WishListItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WishListItemsWishListItemID",
                table: "WishListItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_WishListItemsWishListItemID",
                table: "WishListItems",
                column: "WishListItemsWishListItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_WishListItems_WishListItemsWishListItemID",
                table: "WishListItems",
                column: "WishListItemsWishListItemID",
                principalTable: "WishListItems",
                principalColumn: "WishListItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_WishListItems_WishListItemsWishListItemID",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_WishListItemsWishListItemID",
                table: "WishListItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WishListItems");

            migrationBuilder.DropColumn(
                name: "WishListItemsWishListItemID",
                table: "WishListItems");
        }
    }
}
