using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiUFU.Migrations
{
    /// <inheritdoc />
    public partial class AddDisciplinaCargaHoraria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CargaHoraria",
                table: "Disciplinas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "IdDisciplina", "Obrigatoria", "Periodo", "Titulo" },
                values: new object[,]
                {
                    { "GSI001", 60, 1, null, true, 1, "Empreendedorismo em Informática" },
                    { "GSI002", 60, 1, null, true, 1, "Introdução à Programação de Computadores" },
                    { "GSI003", 60, 1, null, true, 1, "Introdução aos Sistemas de Informação" },
                    { "GSI004", 60, 1, null, true, 1, "Programação Funcional" },
                    { "GSI005", 60, 1, null, true, 1, "Lógica para Computação" },
                    { "GSI006", 60, 1, "GSI002", true, 2, "Estrutura de Dados 1" },
                    { "GSI007", 60, 1, null, true, 2, "Matemática 1" },
                    { "GSI008", 60, 1, null, true, 2, "Sistemas Digitais" },
                    { "GSI009", 60, 1, null, true, 2, "Profissão em Sistemas de Informação" },
                    { "GSI010", 60, 1, "GSI005", true, 2, "Programação Lógica" },
                    { "GSI011", 60, 1, "GSI006", true, 3, "Estrutura de Dados 2" },
                    { "GSI012", 60, 1, "GSI007", true, 3, "Matemática 2" },
                    { "GSI013", 60, 1, "GSI008", true, 3, "Arquitetura e Organização de Computadores" },
                    { "GSI014", 60, 1, null, true, 3, "Matemática para Ciência da Computação" },
                    { "GSI015", 60, 1, null, true, 3, "Programação Orientada a Objetos 1" },
                    { "GSI016", 60, 1, null, true, 4, "Banco de Dados 1" },
                    { "GSI017", 60, 1, null, true, 4, "Estatística" },
                    { "GSI018", 60, 1, "GSI013", true, 4, "Sistemas Operacionais" },
                    { "GSI019", 60, 1, null, true, 4, "Programação para Internet" },
                    { "GSI020", 60, 1, "GSI015", true, 4, "Programação Orientada a Objetos 2" },
                    { "GSI021", 60, 1, "GSI016", true, 5, "Banco de Dados 2" },
                    { "GSI022", 60, 1, null, true, 5, "Matemática Financeira e Análise de Investimentos" },
                    { "GSI023", 60, 1, "GSI018", true, 5, "Redes de Computadores" },
                    { "GSI024", 60, 1, null, true, 5, "Organização e Recuperação da Informação" },
                    { "GSI025", 60, 1, null, true, 5, "Modelagem de Software" },
                    { "GSI026", 60, 1, null, true, 6, "Gestão Empresarial" },
                    { "GSI027", 60, 1, null, true, 6, "Otimização" },
                    { "GSI028", 60, 1, "GSI023", true, 6, "Sistemas Distribuídos" },
                    { "GSI029", 60, 1, null, true, 6, "Contabilidade e Análise de Balanços" },
                    { "GSI030", 60, 1, "GSI025", true, 6, "Engenharia de Software" },
                    { "GSI031", 60, 1, null, true, 7, "Economia" },
                    { "GSI032", 60, 1, null, true, 7, "Fundamentos de Marketing" },
                    { "GSI033", 60, 1, null, true, 7, "Gerência de Projetos de Tecnologia da Informação" },
                    { "GSI034", 60, 1, "GSI030", true, 7, "Projeto e Desenvolvimento de Sistemas de Informação 1" },
                    { "GSI035", 60, 1, null, true, 8, "Auditoria e Segurança da Informação" },
                    { "GSI036", 60, 1, null, true, 8, "Direito e Legislação" },
                    { "GSI037", 60, 1, null, true, 8, "Interação Humano-Computador" },
                    { "GSI038", 60, 1, "GSI034", true, 8, "Projeto e Desenvolvimento de Sistemas de Informação 2" },
                    { "GSI039", 60, 1, null, true, 7, "Trabalho de Conclusão de Curso 1" },
                    { "GSI040", 60, 1, "GSI039", true, 8, "Trabalho de Conclusão de Curso 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI001");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI002");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI003");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI004");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI005");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI006");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI007");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI008");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI009");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI010");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI011");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI012");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI013");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI014");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI015");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI016");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI017");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI018");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI019");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI020");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI021");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI022");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI023");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI024");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI025");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI026");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI027");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI028");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI029");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI030");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI031");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI032");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI033");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI034");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI035");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI036");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI037");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI038");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI039");

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: "GSI040");

            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Disciplinas");
        }
    }
}
