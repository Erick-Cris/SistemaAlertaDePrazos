using ApiAlertaDePrazos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiAlertaDePrazos.Data
{
    public class SistemaDeAlertaDePrazosContext : IdentityDbContext
    {

        public DbSet<Regra> Regras { get; set; }
        public DbSet<Alerta> Alertas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AlertaDePrazos;Trusted_Connection=True");
        }
    }
}
