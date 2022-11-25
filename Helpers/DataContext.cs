using meu_financeiro.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace meu_financeiro.API.Helpers
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("MeuFinanceiroDataBase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<CategoriasReceitas> CategoriasReceitas { get; set; }
        public DbSet<CategoriasDespesas> CategoriasDespesas { get; set; }
        public DbSet<Contas> Contas { get; set; }
        public DbSet<Despesas> Despesas { get; set; }
        public DbSet<Receitas> Receitas { get; set; }
        public DbSet<Transferencias> Transferencias { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
