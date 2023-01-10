using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiUFU.Migrations
{
    /// <inheritdoc />
    public partial class AddEvolucaoSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Nota",
                table: "MatriculaDisciplinas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Trancamento",
                table: "MatriculaDisciplinas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trancamento",
                table: "MatriculaDisciplinas");

            migrationBuilder.AlterColumn<int>(
                name: "Nota",
                table: "MatriculaDisciplinas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
