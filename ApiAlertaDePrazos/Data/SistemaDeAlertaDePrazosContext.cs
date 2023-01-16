
using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using Microsoft.EntityFrameworkCore;

namespace ApiAlertaDePrazos.Data
{
    public class SistemaDeAlertaDePrazosContext : DbContext
    {

        public DbSet<Regra> Regras { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AlertaDePrazos;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
               new Usuario
               {
                   Id = 1,
                   Nome = "Administrador",
                   Email = "erickcristianup@gmail.com",
                   IsActive = true,
                   PasswordHash = "123456"
               });

           modelBuilder.Entity<Regra>().HasData(
                new Regra
                {
                    Id = 1,
                    Nome = "Rendimento",
                    Descricao = "Regra que define os critérios de avaliação do rendimento do discente",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 2,
                    Nome = "Trancamento Parical Ativo",
                    Descricao = "Regra que verifica se o aluno já trancou alguma disciplina",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 3,
                    Nome = "Trancamento Geral Ativo",
                    Descricao = "Regra que verifica se o aluno já realizou trancamento geral do semestre",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 4,
                    Nome = "Trancamento Parcial Passivo",
                    Descricao = "Notifica os alunos sobre os requisitos e limites previstos em norma para realizar trancamento parcial.",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 5,
                    Nome = "Trancamento Geral Passivo",
                    Descricao = "Notifica os alunos sobre os requisitos e limites previstos em norma para realizar trancamento geral.",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 6,
                    Nome = "Estagio Obrigatório Possível",
                    Descricao = "Verifica se o discente já atende aos requisitos previstos nas normas da graduação para poder dar início ao estágio obrigatório.",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 7,
                    Nome = "Estagio Não Obrigatório Possível",
                    Descricao = "Verifica se o discente já atende aos requisitos previstos nas normas da graduação para poder dar início ao estágio não obrigatório.",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 8,
                    Nome = "Estagio Obrigatório Ativo",
                    Descricao = "Verifica se o aluno está com estágio obrigatório em andamento para notifica-lo sobre prazos em relação ao estágio.",
                    IsActive = true,
                    Parametros = String.Empty
                },
                new Regra
                {
                    Id = 9,
                    Nome = "Estagio Não Obrigatório Ativo",
                    Descricao = "Verifica se o aluno está com estágio não obrigatório em andamento para notifica-lo sobre prazos em relação ao estágio.",
                    IsActive = true,
                    Parametros = String.Empty
                }
                );
        }
    }
}
