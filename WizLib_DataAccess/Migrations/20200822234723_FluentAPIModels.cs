using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class FluentAPIModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fluent_Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Fluent_Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Fluent_Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BookDetailId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    PriceRange = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Fluent_Books_Fluent_BookDetails_BookDetailId",
                        column: x => x.BookDetailId,
                        principalTable: "Fluent_BookDetails",
                        principalColumn: "BookDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fluent_Books_Fluent_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Fluent_Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_BookDetailId",
                table: "Fluent_Books",
                column: "BookDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_PublisherId",
                table: "Fluent_Books",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_Authors");

            migrationBuilder.DropTable(
                name: "Fluent_Books");

            migrationBuilder.DropTable(
                name: "Fluent_Publishers");
        }
    }
}
