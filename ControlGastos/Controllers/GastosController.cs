using ControlGastos.context.dtos;
using ControlGastos.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController(IGastoRepository repository) : ControllerBase
    {
        private readonly IGastoRepository _repository = repository;

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] GastoRegistroDTO dto)
        {
            var resultado = await _repository.RegistrarGastoAsync(dto);
            if (resultado.TieneSobregiro)
                return Ok(new { mensaje = "Gasto registrado, pero con sobregiros", resultado });

            return Ok(resultado);
        }

        [HttpGet("movimientos")]
        public async Task<IActionResult> Movimientos([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var datos = await _repository.ObtenerGastosAsync(fechaInicio, fechaFin);
            return Ok(datos);
        }
    }
}
