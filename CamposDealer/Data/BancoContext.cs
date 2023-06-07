using CamposDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CamposDealer.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<VendaModel> Venda { get; set; }

    }
}
