using ExpenseTrackerAPI.Data.Repositories;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Dtos.Categoria;
using ExpenseTrackerAPI.Dtos.Gasto;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Services
{
    public interface IGastoService
    {
        Task<IEnumerable<GastoDTO>> GetAllGastos(int usuarioID, string filtrarHasta, DateTime? fechaInicio, DateTime? fechaFin);
        Task<GastoDTO> GetGasto(int usuarioID, int id);
        Task<ServiceResult<GastoDTO>> AddGasto(GastoOperacionDTO gastoCreacion, int usuarioID);
        Task<ServiceResult<GastoDTO>> UpdateGasto(int usuarioID, int Id, GastoOperacionDTO gastoModificacion);
        Task<ServiceResult<GastoDTO>> DeleteGasto(int usuarioID, int id);
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

        public async Task<IEnumerable<GastoDTO>> GetAllGastos(int usuarioId, string filtrarHasta, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = await _gastoRepository.GetAllGastos(usuarioId);

            DateTime fechaHoy = DateTime.Now;

            if (!string.IsNullOrEmpty(filtrarHasta))
            {
                switch (filtrarHasta)
                {
                    case "semanapasada":
                        query = query.Where(g => g.FechaGasto >= fechaHoy.AddDays(-7));
                        break;
                    case "mespasado":
                        query = query.Where(g => g.FechaGasto >= fechaHoy.AddMonths(-1));
                        break;
                    case "hace3meses":
                        query = query.Where(g => g.FechaGasto >= fechaHoy.AddMonths(-3));
                        break;
                    default:
                        // Por defecto filtrar todos los gastos
                        break;
                }
            }
            else if (fechaInicio.HasValue && fechaFin.HasValue)
            { 
                query = query.Where(g => g.FechaGasto >= fechaInicio && g.FechaGasto <= fechaFin);
            }

            var gastos = await query.ToListAsync();
            
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

        public async Task<GastoDTO> GetGasto(int usuarioID, int id)
        {
            var gasto = await _gastoRepository.GetGasto(usuarioID, id);

            if (gasto == null)
            {
                return null;
            }

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

        public async Task<ServiceResult<GastoDTO>> AddGasto(GastoOperacionDTO gastoCreacionDTO, int usuarioID)
        {
            // Va

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

        public async Task<ServiceResult<GastoDTO>> UpdateGasto(int usuarioID, int Id, GastoOperacionDTO gastoModificacionDTO)
        {
            // Validar que el gasto exista
            var gasto = await _gastoRepository.GetGasto(usuarioID, Id);
            if(gasto == null)
            {
                return ServiceResult<GastoDTO>.ErrorResult("El gasto no existe");
            }

            // Validar que la categoria exista
            var categoria = _categoriaRepository.GetCategoria(gastoModificacionDTO.CategoriaId);
            if (categoria == null)
            {
                return ServiceResult<GastoDTO>.ErrorResult("La categoria no existe");
            }

            gasto.CategoriaId = gastoModificacionDTO.CategoriaId;
            gasto.Descripcion = gastoModificacionDTO.Descripcion;
            gasto.FechaGasto = gastoModificacionDTO.FechaGasto;
            gasto.Monto = gastoModificacionDTO.Monto;

            await _gastoRepository.UpdateGasto(gasto);

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

        public async Task<ServiceResult<GastoDTO>> DeleteGasto(int usuarioID, int id)
        {
            var gasto = await _gastoRepository.GetGasto(usuarioID, id);
            if (gasto == null)
            {
                return ServiceResult<GastoDTO>.ErrorResult("El gasto no existe");
            }

            await _gastoRepository.DeleteGasto(gasto);

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
    }
}
