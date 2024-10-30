using ExpenseTrackerAPI.Data.Repositories;
using ExpenseTrackerAPI.Dtos.Categoria;

namespace ExpenseTrackerAPI.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetAllCategorias();
    }

    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllCategorias()
        {
            var categorias = await _categoriaRepository.GetCategorias();
            
            return categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre
            });
        }
    }
}
