using ControlGastos.context.dtos;
using ControlGastos.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    public class GastoController(IGastoRepository repository) : ControllerBase
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
        public async Task<IActionResult> Movimientos([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            var datos = await _repository.ObtenerMovimientosAsync(desde, hasta);
            return Ok(datos);
        }
    }
}
