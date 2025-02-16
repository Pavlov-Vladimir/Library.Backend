using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
