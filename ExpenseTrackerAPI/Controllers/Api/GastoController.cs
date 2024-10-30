﻿using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Dtos.Gasto;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllGastos()
        {
            var gastos = await _gastoService.GetAllGastos();
            return Ok(gastos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGasto(int id)
        {
            var gasto = await _gastoService.GetGasto(id);
            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }

        [HttpPost]
        public async Task<IActionResult> AddGasto([FromBody] GastoCreacionDTO gastoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioID = int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));

            var result = await _gastoService.AddGasto(gastoDTO, usuarioID);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetGasto), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGasto(int id, [FromBody] GastoBorrarDTO gastoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _gastoService.UpdateGasto(id, gastoDTO);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var result = await _gastoService.DeleteGasto(id);

            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}