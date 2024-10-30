using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetCategoria(int id);
    }
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> GetCategoria(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
