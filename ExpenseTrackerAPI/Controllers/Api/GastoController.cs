using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Dtos.Gasto;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastoController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGastos(
            [FromQuery] string filtrarHasta = null,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null)
        {
            // Obtener el usuario Id del token
            var usuarioID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var gastos = await _gastoService.GetAllGastos(usuarioID, filtrarHasta, fechaInicio, fechaFin);
            return Ok(gastos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGasto(int id)
        {
            // Obtener el usuario Id del token
            var usuarioID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var gasto = await _gastoService.GetGasto(usuarioID, id);
            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }

        [HttpPost]
        public async Task<IActionResult> AddGasto([FromBody] GastoOperacionDTO gastoCreacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el usuario Id del token
            var usuarioID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _gastoService.AddGasto(gastoCreacionDTO, usuarioID);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetGasto), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGasto(int id, [FromBody] GastoOperacionDTO gastoModificacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el usuario Id del token
            var usuarioID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _gastoService.UpdateGasto(usuarioID, id, gastoModificacionDTO);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            // Obtener el usuario Id del token
            var usuarioID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _gastoService.DeleteGasto(usuarioID, id);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
