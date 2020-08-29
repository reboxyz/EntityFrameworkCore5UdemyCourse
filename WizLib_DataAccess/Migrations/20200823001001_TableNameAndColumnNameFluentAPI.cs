using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class TableNameAndColumnNameFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_SampleAnnotations",
                table: "tb_SampleAnnotations");

            migrationBuilder.RenameTable(
                name: "tb_SampleAnnotations",
                newName: "tbl_SampleAnnotations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_SampleAnnotations",
                table: "tbl_SampleAnnotations",
                column: "SampleAnnotation_Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_SampleAnnotations",
                table: "tbl_SampleAnnotations");

            migrationBuilder.RenameTable(
                name: "tbl_SampleAnnotations",
                newName: "tb_SampleAnnotations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_SampleAnnotations",
                table: "tb_SampleAnnotations",
                column: "SampleAnnotation_Id");
        }
    }
}
