using ExpenseTrackerAPI.Data.Repositories;
using ExpenseTrackerAPI.Dtos.Usuario;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        private readonly ITokenService _tokenService;

        public CuentaController(ICuentaService cuentaService, ITokenService tokenService)
        {
            _cuentaService = cuentaService;
            _tokenService = tokenService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cuentaService.Registrar(usuarioDTO);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cuentaService.Login(usuarioLoginDTO);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            var token = _tokenService.GenerarToken(result.Data.Id, result.Data.Nombre);
            return Ok(new { Token = token });
        }
     
    }
}
