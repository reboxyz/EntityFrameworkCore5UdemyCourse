using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddManyToManyBookAndAuthorFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fluent_BookAuthor",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_BookAuthor", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Fluent_BookAuthor_Fluent_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Fluent_Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fluent_BookAuthor_Fluent_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Fluent_Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_BookAuthor_BookId",
                table: "Fluent_BookAuthor",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_BookAuthor");
        }
    }
}
