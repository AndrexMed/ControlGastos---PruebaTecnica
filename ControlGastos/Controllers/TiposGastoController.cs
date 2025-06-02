using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposGastoController(ITipoGastoRepository repository) : ControllerBase
    {
        private readonly ITipoGastoRepository _repository = repository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoGasto>>> Get()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoGasto>> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TipoGasto>> Create(TipoGasto tipoGasto)
        {
            var created = await _repository.CreateAsync(tipoGasto);
            return CreatedAtAction(nameof(GetById), new { id = created.TipoGastoId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoGasto tipoGasto)
        {
            if (id != tipoGasto.TipoGastoId)
                return BadRequest();

            await _repository.UpdateAsync(tipoGasto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
