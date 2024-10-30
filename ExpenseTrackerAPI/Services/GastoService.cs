using ExpenseTrackerAPI.Data.Repositories;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Dtos.Categoria;
using ExpenseTrackerAPI.Dtos.Gasto;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface IGastoService
    {
        Task<IEnumerable<GastoDTO>> GetAllGastos();
        Task<GastoDTO> GetGasto(int id);
        Task<ServiceResult<GastoDTO>> AddGasto(GastoCreacionDTO gasto, int usuarioID);
        Task<ServiceResult<Gasto>> UpdateGasto(int Id, GastoBorrarDTO gastoDTO);
        Task<ServiceResult<Gasto>> DeleteGasto(int id);
    }

    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _gastoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GastoService(IGastoRepository gastoRepository, ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository)
        {
            _gastoRepository = gastoRepository;
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<GastoDTO>> GetAllGastos()
        {
            var gastos = await _gastoRepository.GetAllGastos();
            
            return gastos.Select(g => new GastoDTO
            {
                Id = g.Id,
                Descripcion = g.Descripcion,
                FechaGasto = g.FechaGasto,
                Monto = g.Monto,
                FechaCreacion = g.FechaCreacion,
                Categoria = new CategoriaDTO
                {
                    Id = g.Categoria.Id,
                    Nombre = g.Categoria.Nombre
                }
            });
        }

        public async Task<GastoDTO> GetGasto(int id)
        {
            var gasto = await _gastoRepository.GetGasto(id);

            return new GastoDTO
            {
                Id = gasto.Id,
                Descripcion = gasto.Descripcion,
                FechaGasto = gasto.FechaGasto,
                Monto = gasto.Monto,
                FechaCreacion = gasto.FechaCreacion,
                Categoria = new CategoriaDTO
                {
                    Id = gasto.Categoria.Id,
                    Nombre = gasto.Categoria.Nombre
                }
            };
        }

        public async Task<ServiceResult<GastoDTO>> AddGasto(GastoCreacionDTO gastoCreacionDTO, int usuarioID)
        {
            // Validar que la categoria exista
            var categoria = _categoriaRepository.GetCategoria(gastoCreacionDTO.CategoriaId);
            if (categoria == null)
            {
                return ServiceResult<GastoDTO>.ErrorResult("La categoria no existe");
            }

            // Validar que el usuario exista
            var usuario = _usuarioRepository.GetUsuario(usuarioID);
            if (usuario == null)
            {
                return ServiceResult<GastoDTO>.ErrorResult("El usuario no existe");
            }

            var gasto = new Gasto
            {
                UsuarioId = usuarioID,
                CategoriaId = gastoCreacionDTO.CategoriaId,
                Descripcion = gastoCreacionDTO.Descripcion,
                FechaGasto = gastoCreacionDTO.FechaGasto,
                Monto = gastoCreacionDTO.Monto
            };


            await _gastoRepository.AddGasto(gasto);

            var gastoDTO = new GastoDTO
            {
                Id = gasto.Id,
                Descripcion = gasto.Descripcion,
                FechaGasto = gasto.FechaGasto,
                Monto = gasto.Monto,
                FechaCreacion = gasto.FechaCreacion,
                Categoria = new CategoriaDTO
                {
                    Id = gasto.Categoria.Id,
                    Nombre = gasto.Categoria.Nombre
                }
            };

            return ServiceResult<GastoDTO>.SuccessResult(gastoDTO);
        }

        public async Task<ServiceResult<Gasto>> UpdateGasto(int Id, GastoBorrarDTO gastoDTO)
        {
            // Validar que el gasto exista
            var gasto = await _gastoRepository.GetGasto(Id);
            if(gasto == null)
            {
                return ServiceResult<Gasto>.ErrorResult("El gasto no existe");
            }

            // Validar que la categoria exista
            var categoria = _categoriaRepository.GetCategoria(gastoDTO.CategoriaId);
            if (categoria == null)
            {
                return ServiceResult<Gasto>.ErrorResult("La categoria no existe");
            }

            gasto.CategoriaId = gastoDTO.CategoriaId;
            gasto.Descripcion = gastoDTO.Descripcion;
            gasto.FechaGasto = gastoDTO.FechaGasto;
            gasto.Monto = gastoDTO.Monto;

            await _gastoRepository.UpdateGasto(gasto);
            return ServiceResult<Gasto>.SuccessResult(gasto);
        }

        public async Task<ServiceResult<Gasto>> DeleteGasto(int id)
        {
            var gasto = await _gastoRepository.GetGasto(id);
            if (gasto == null)
            {
                return ServiceResult<Gasto>.ErrorResult("El gasto no existe");
            }

            await _gastoRepository.DeleteGasto(gasto);
            return ServiceResult<Gasto>.SuccessResult(gasto);
        }
    }
}
