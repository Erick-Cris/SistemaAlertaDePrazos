using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiAlertaDePrazos.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegraId = table.Column<int>(type: "int", nullable: false),
                    MatriculaAluno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAlerta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parametros = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regras", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Regras",
                columns: new[] { "Id", "Descricao", "IsActive", "Nome", "Parametros" },
                values: new object[,]
                {
                    { 1, "Regra que define os critérios de avaliação do rendimento do discente", true, "Rendimento", "" },
                    { 2, "Regra que verifica se o aluno já trancou alguma disciplina", true, "Trancamento Parical Ativo", "" },
                    { 3, "Regra que verifica se o aluno já realizou trancamento geral do semestre", true, "Trancamento Geral Ativo", "" },
                    { 4, "Notifica os alunos sobre os requisitos e limites previstos em norma para realizar trancamento parcial.", true, "Trancamento Parcial Passivo", "" },
                    { 5, "Notifica os alunos sobre os requisitos e limites previstos em norma para realizar trancamento geral.", true, "Trancamento Geral Passivo", "" },
                    { 6, "Verifica se o discente já atende aos requisitos previstos nas normas da graduação para poder dar início ao estágio obrigatório.", true, "Estagio Obrigatório Possível", "" },
                    { 7, "Verifica se o discente já atende aos requisitos previstos nas normas da graduação para poder dar início ao estágio não obrigatório.", true, "Estagio Não Obrigatório Possível", "" },
                    { 8, "Verifica se o aluno está com estágio obrigatório em andamento para notifica-lo sobre prazos em relação ao estágio.", true, "Estagio Obrigatório Ativo", "" },
                    { 9, "Verifica se o aluno está com estágio não obrigatório em andamento para notifica-lo sobre prazos em relação ao estágio.", true, "Estagio Não Obrigatório Ativo", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Regras");
        }
    }
}
