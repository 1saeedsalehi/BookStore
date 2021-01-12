using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Data.Migrations
{
    public partial class listin_book_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Listings_ListingId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ListingId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "ListingBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingBooks_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingBooks_BookId",
                table: "ListingBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingBooks_ListingId",
                table: "ListingBooks",
                column: "ListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingBooks");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ListingId",
                table: "Books",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Listings_ListingId",
                table: "Books",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
