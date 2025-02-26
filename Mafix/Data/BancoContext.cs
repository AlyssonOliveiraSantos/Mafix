using Mafix.Models;
using Microsoft.EntityFrameworkCore;

namespace Mafix.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<MaquinaModel> Maquinas { get; set; }
        public DbSet<OperadorModel> Operadores { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProducaoModel> Producao { get; set; }
        public DbSet<ParadaMaquinaModel> ParadaMaquina { get; set; }
    }
}
