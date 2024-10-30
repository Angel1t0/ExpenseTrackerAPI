using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategorias();
        Task<Categoria> GetCategoria(int id);
    }
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetCategoria(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
