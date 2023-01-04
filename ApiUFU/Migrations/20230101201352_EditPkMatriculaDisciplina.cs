using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiUFU.Migrations
{
    /// <inheritdoc />
    public partial class EditPkMatriculaDisciplina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MatriculaDisciplinas",
                table: "MatriculaDisciplinas");

            migrationBuilder.AlterColumn<string>(
                name: "DisciplinaId",
                table: "MatriculaDisciplinas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AlunoId",
                table: "MatriculaDisciplinas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MatriculaDisciplinas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatriculaDisciplinas",
                table: "MatriculaDisciplinas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MatriculaDisciplinas",
                table: "MatriculaDisciplinas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MatriculaDisciplinas");

            migrationBuilder.AlterColumn<string>(
                name: "DisciplinaId",
                table: "MatriculaDisciplinas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AlunoId",
                table: "MatriculaDisciplinas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatriculaDisciplinas",
                table: "MatriculaDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId" });
        }
    }
}
