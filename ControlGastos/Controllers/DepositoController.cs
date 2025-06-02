using ControlGastos.context.dtos;
using ControlGastos.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController(IDepositoRepository depositoRepository) : ControllerBase
    {
        private readonly IDepositoRepository _depositoRepository = depositoRepository;

        [HttpPost("registrar-deposito")]
        public async Task<IActionResult> RegistrarDeposito([FromBody] DepositoRegistroDTO dto)
        {
            if (dto == null || dto.Monto <= 0 || dto.FondoMonetarioId <= 0)
            {
                return BadRequest("Datos de depósito inválidos.");
            }

            var depositoId = await _depositoRepository.RegistrarDepositoAsync(dto);
            return Ok(new { DepositoId = depositoId });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDepositos(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }
            var depositos = await _depositoRepository.ObtenerDepositosAsync(fechaInicio, fechaFin);
            return Ok(depositos);
        }
    }
}