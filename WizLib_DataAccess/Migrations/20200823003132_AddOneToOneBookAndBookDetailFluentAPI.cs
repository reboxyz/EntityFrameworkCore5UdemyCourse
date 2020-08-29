using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddOneToOneBookAndBookDetailFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_Books_Fluent_Publishers_PublisherId",
                table: "Fluent_Books");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_Books_BookDetailId",
                table: "Fluent_Books");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_Books_PublisherId",
                table: "Fluent_Books");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Fluent_Books");

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_BookDetailId",
                table: "Fluent_Books",
                column: "BookDetailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fluent_Books_BookDetailId",
                table: "Fluent_Books");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Fluent_Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_BookDetailId",
                table: "Fluent_Books",
                column: "BookDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_PublisherId",
                table: "Fluent_Books",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_Books_Fluent_Publishers_PublisherId",
                table: "Fluent_Books",
                column: "PublisherId",
                principalTable: "Fluent_Publishers",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
