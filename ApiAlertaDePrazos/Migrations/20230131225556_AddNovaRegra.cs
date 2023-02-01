using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAlertaDePrazos.Migrations
{
    /// <inheritdoc />
    public partial class AddNovaRegra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 6,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 7,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 8,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 9,
                column: "Parametros",
                value: "[1]");

            migrationBuilder.InsertData(
                table: "Regras",
                columns: new[] { "Id", "Descricao", "IsActive", "Nome", "Parametros" },
                values: new object[] { 10, "Verifica se o aluno está matriculado em 2 ou menos curriculares para notifica-lo de que ao realizar trancam.", true, "Limite mínimo de matrícula em disciplinas", "[1]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 6,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 7,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 8,
                column: "Parametros",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 9,
                column: "Parametros",
                value: "[]");
        }
    }
}
