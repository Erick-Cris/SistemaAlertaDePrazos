using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAlertaDePrazos.Migrations
{
    /// <inheritdoc />
    public partial class AddAjusteSenhaUsuarioAmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "7C4A8D09CA3762AF61E59520943DC26494F8941B");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "123456");
        }
    }
}
