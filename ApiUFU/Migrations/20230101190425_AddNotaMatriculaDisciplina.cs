using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiUFU.Migrations
{
    /// <inheritdoc />
    public partial class AddNotaMatriculaDisciplina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nota",
                table: "MatriculaDisciplinas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "MatriculaDisciplinas");
        }
    }
}
