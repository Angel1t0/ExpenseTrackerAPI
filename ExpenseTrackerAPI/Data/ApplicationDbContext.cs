using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        // Llenar la tabla Categorias
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Comida" },
            new Categoria { Id = 2, Nombre = "Entretenimiento" },
            new Categoria { Id = 3, Nombre = "Electrónica" },
            new Categoria { Id = 4, Nombre = "Utilidades" },
            new Categoria { Id = 5, Nombre = "Ropa" },
            new Categoria { Id = 6, Nombre = "Salud" },
            new Categoria { Id = 7, Nombre = "Otros" });
        }
    }
}
