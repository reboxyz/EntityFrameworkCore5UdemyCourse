using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class ChangeNameFromNameToGenreNameToGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");

            /*
            // Alternative approach but renaming is the most logical; Add column, migrate column values, then drop column
            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.Sql("UPDATE dbo.Genres SET GenreName=Name");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Genres"
            );   
            */ 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Name");
        }
    }
}
