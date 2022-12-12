using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiUFU.Migrations
{
    /// <inheritdoc />
    public partial class SeedDisciplinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSemestre",
                table: "MatriculaDisciplinas",
                newName: "SemestreId");

            migrationBuilder.AddColumn<string>(
                name: "IdDisciplina",
                table: "Disciplinas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Periodo",
                table: "Disciplinas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdDisciplina",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "Disciplinas");

            migrationBuilder.RenameColumn(
                name: "SemestreId",
                table: "MatriculaDisciplinas",
                newName: "IdSemestre");
        }
    }
}
