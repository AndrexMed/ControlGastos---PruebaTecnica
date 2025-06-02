using ControlGastos.context.dtos;
using ControlGastos.repository.implementation;
using ControlGastos.repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraficosController(IGraficosRepository repository) : ControllerBase
    {
        private readonly IGraficosRepository _repository = repository;

        [HttpGet("comparativo")]
        public async Task<ActionResult<List<ComparativoDTO>>> GetComparativo([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var resultado = await _repository.ObtenerComparativoAsync(fechaInicio, fechaFin);
            return Ok(resultado);
        }

    }
}