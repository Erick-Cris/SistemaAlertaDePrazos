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
    }
}
