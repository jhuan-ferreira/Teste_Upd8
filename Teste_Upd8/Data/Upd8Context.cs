using Microsoft.EntityFrameworkCore;
using Teste_Upd8.Models;

namespace Teste_Upd8.Data
{
    public class Upd8Context : DbContext
    {
        public Upd8Context(DbContextOptions<Upd8Context> options)
            : base(options)
        { 
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
