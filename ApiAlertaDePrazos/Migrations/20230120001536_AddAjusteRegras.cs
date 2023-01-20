using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAlertaDePrazos.Migrations
{
    /// <inheritdoc />
    public partial class AddAjusteRegras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 6,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 7,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 8,
                column: "Parametros",
                value: "");

            migrationBuilder.UpdateData(
                table: "Regras",
                keyColumn: "Id",
                keyValue: 9,
                column: "Parametros",
                value: "");
        }
    }
}
