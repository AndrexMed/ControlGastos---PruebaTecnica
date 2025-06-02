using ControlGastos.context.dtos;
using ControlGastos.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoController(IPresupuestoRepository repository) : ControllerBase
    {
        private readonly IPresupuestoRepository _repository = repository;

        [HttpPost]
        public async Task<IActionResult> GuardarPresupuesto([FromBody] PresupuestoDTO dto)
        {
            if (dto.Monto < 0) return BadRequest("El monto no puede ser negativo.");

            await _repository.CrearOActualizarPresupuestoAsync(dto);
            return Ok(new { mensaje = "Presupuesto guardado correctamente" });
        }

        [HttpGet("{tipoGastoId}/{mes}/{anio}")]
        public async Task<IActionResult> Obtener(int tipoGastoId, int mes, int anio)
        {
            var presupuesto = await _repository.ObtenerPresupuestoAsync(tipoGastoId, mes, anio);
            return presupuesto == null ? NotFound() : Ok(presupuesto);
        }

        [HttpGet("GetallPresupuesto")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _repository.ObtenerTodosAsync();
            return Ok(lista);
        }
    }
}
