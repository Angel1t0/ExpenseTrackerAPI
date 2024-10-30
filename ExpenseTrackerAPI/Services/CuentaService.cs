using ExpenseTrackerAPI.Data.Repositories;
using ExpenseTrackerAPI.Dtos.Usuario;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface ICuentaService
    {
        Task<ServiceResult<UsuarioRespuestaDTO>> Registrar(UsuarioRegistroDTO usuarioDTO);
        Task<ServiceResult<UsuarioLoginRespuestaDTO>> Login(UsuarioLoginDTO usuarioLoginDTO);
    }
    public class CuentaService : ICuentaService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CuentaService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ServiceResult<UsuarioRespuestaDTO>> Registrar(UsuarioRegistroDTO usuarioDTO)
        {
            // Validar que el usuario no exista
            var usuarioExistente = await _usuarioRepository.GetUsuarioByEmail(usuarioDTO.Email);
            if (usuarioExistente != null)
            {
                return ServiceResult<UsuarioRespuestaDTO>.ErrorResult("El usuario ya existe");
            }

            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                Email = usuarioDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Password)
            };

            await _usuarioRepository.AddUsuario(usuario);

            var usuarioRespuesta = new UsuarioRespuestaDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion
            };

            return ServiceResult<UsuarioRespuestaDTO>.SuccessResult(usuarioRespuesta);
        }

        public async Task<ServiceResult<UsuarioLoginRespuestaDTO>> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            // Validar que el usuario exista
            var usuario = await _usuarioRepository.GetUsuarioByEmail(usuarioLoginDTO.Email);
            if (usuario == null)
            {
                return ServiceResult<UsuarioLoginRespuestaDTO>.ErrorResult("El usuario no existe");
            }

            // Validar que la contraseña sea correcta
            if (!BCrypt.Net.BCrypt.Verify(usuarioLoginDTO.Password, usuario.Password))
            {
                return ServiceResult<UsuarioLoginRespuestaDTO>.ErrorResult("La contraseña es incorrecta");
            }

            var usuarioRespuesta = new UsuarioLoginRespuestaDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre
            };

            return ServiceResult<UsuarioLoginRespuestaDTO>.SuccessResult(usuarioRespuesta);
        }
    }
}
