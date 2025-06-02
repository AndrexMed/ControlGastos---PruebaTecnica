using Microsoft.AspNetCore.Mvc;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;

namespace ControlGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FondoMonetarioController(IFondoMonetarioRepository repository) : ControllerBase
    {
        private readonly IFondoMonetarioRepository _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fondos = await _repository.GetAllAsync();
            return Ok(fondos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fondo = await _repository.GetByIdAsync(id);
            if (fondo == null) return NotFound();
            return Ok(fondo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FondoMonetario fondo)
        {
            var creado = await _repository.AddAsync(fondo);
            return CreatedAtAction(nameof(GetById), new { id = creado.FondoMonetarioId }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FondoMonetario fondo)
        {
            if (id != fondo.FondoMonetarioId) return BadRequest();

            var actualizado = await _repository.UpdateAsync(fondo);
            if (actualizado == null) return NotFound();

            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repository.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
