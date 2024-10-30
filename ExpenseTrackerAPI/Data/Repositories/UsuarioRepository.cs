using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuario(int id);
        Task<Usuario> GetUsuarioByEmail(string email);
        Task AddUsuario(Usuario usuario);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
