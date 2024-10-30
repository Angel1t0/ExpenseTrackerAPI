using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public interface IGastoRepository
    {
        Task<IEnumerable<Gasto>> GetAllGastos();
        Task<Gasto> GetGasto(int id);
        Task AddGasto(Gasto gasto);
        Task UpdateGasto(Gasto gasto);
        Task DeleteGasto(Gasto gasto);
    }
    public class GastoRepository : IGastoRepository
    {
        private readonly ApplicationDbContext _context;

        public GastoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gasto>> GetAllGastos()
        {
            return await _context.Gastos.Include(g => g.Categoria).ToListAsync();
        }

        public async Task<Gasto> GetGasto(int id)
        {
            var gasto = await _context.Gastos
            .Include(g => g.Categoria)
            .FirstOrDefaultAsync(g => g.Id == id);

            return gasto;
        }

        public async Task AddGasto(Gasto gasto)
        {
            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGasto(Gasto gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGasto(Gasto gasto)
        {
            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
        }
    }
}
