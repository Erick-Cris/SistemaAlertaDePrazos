using ApiUFU.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUFU.Data
{
    public class UFUContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Semestre> Semestres { get; set; }
        public DbSet<Estagio> Estagios { get; set; }
        public DbSet<MatriculaDisciplina> MatriculaDisciplinas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UFU;Trusted_Connection=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disciplina>().HasData(
                new Disciplina
                {
                    Id = "GSI001",
                    Titulo = "Empreendedorismo em Informática",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Primeiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI002",
                    Titulo = "Introdução à Programação de Computadores",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Primeiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI003",
                    Titulo = "Introdução aos Sistemas de Informação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Primeiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI004",
                    Titulo = "Programação Funcional",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Primeiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI005",
                    Titulo = "Lógica para Computação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Primeiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI006",
                    Titulo = "Estrutura de Dados 1",
                    IdDisciplina = "GSI002",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Segundo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI007",
                    Titulo = "Matemática 1",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Segundo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI008",
                    Titulo = "Sistemas Digitais",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Segundo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI009",
                    Titulo = "Profissão em Sistemas de Informação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Segundo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI010",
                    Titulo = "Programação Lógica",
                    IdDisciplina = "GSI005",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Segundo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI011",
                    Titulo = "Estrutura de Dados 2",
                    IdDisciplina = "GSI006",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Terceiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI012",
                    Titulo = "Matemática 2",
                    IdDisciplina = "GSI007",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Terceiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI013",
                    Titulo = "Arquitetura e Organização de Computadores",
                    IdDisciplina = "GSI008",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Terceiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI014",
                    Titulo = "Matemática para Ciência da Computação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Terceiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI015",
                    Titulo = "Programação Orientada a Objetos 1",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Terceiro,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI016",
                    Titulo = "Banco de Dados 1",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quarto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI017",
                    Titulo = "Estatística",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quarto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI018",
                    Titulo = "Sistemas Operacionais",
                    IdDisciplina = "GSI013",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quarto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI019",
                    Titulo = "Programação para Internet",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quarto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI020",
                    Titulo = "Programação Orientada a Objetos 2",
                    IdDisciplina = "GSI015",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quarto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI021",
                    Titulo = "Banco de Dados 2",
                    IdDisciplina = "GSI016",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quinto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI022",
                    Titulo = "Matemática Financeira e Análise de Investimentos",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quinto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI023",
                    Titulo = "Redes de Computadores",
                    IdDisciplina = "GSI018",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quinto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI024",
                    Titulo = "Organização e Recuperação da Informação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quinto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI025",
                    Titulo = "Modelagem de Software",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Quinto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI026",
                    Titulo = "Gestão Empresarial",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Sexto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI027",
                    Titulo = "Otimização",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Sexto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI028",
                    Titulo = "Sistemas Distribuídos",
                    IdDisciplina = "GSI023",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Sexto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI029",
                    Titulo = "Contabilidade e Análise de Balanços",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Sexto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI030",
                    Titulo = "Engenharia de Software",
                    IdDisciplina = "GSI025",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Sexto,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI031",
                    Titulo = "Economia",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Setimo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI032",
                    Titulo = "Fundamentos de Marketing",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Setimo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI033",
                    Titulo = "Gerência de Projetos de Tecnologia da Informação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Setimo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI034",
                    Titulo = "Projeto e Desenvolvimento de Sistemas de Informação 1",
                    IdDisciplina = "GSI030",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Setimo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI039",
                    Titulo = "Trabalho de Conclusão de Curso 1",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Setimo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI035",
                    Titulo = "Auditoria e Segurança da Informação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Oitavo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI036",
                    Titulo = "Direito e Legislação",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Oitavo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI037",
                    Titulo = "Interação Humano-Computador",
                    IdDisciplina = null,
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Oitavo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI038",
                    Titulo = "Projeto e Desenvolvimento de Sistemas de Informação 2",
                    IdDisciplina = "GSI034",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Oitavo,
                    CargaHoraria = 60
                },
                new Disciplina
                {
                    Id = "GSI040",
                    Titulo = "Trabalho de Conclusão de Curso 2",
                    IdDisciplina = "GSI039",
                    Obrigatoria = true,
                    CursoId = 1,
                    Periodo = Utils.Periodo.Oitavo,
                    CargaHoraria = 60
                }
            );
        }
    }
}
