using ControlGastos.context;
using ControlGastos.context.dtos;
using Microsoft.EntityFrameworkCore;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;

namespace ControlGastos.repository.implementation
{
    public class PresupuestoRepository(ControlGastosContext context) : IPresupuestoRepository
    {
        private readonly ControlGastosContext _context = context;

        public async Task CrearOActualizarPresupuestoAsync(PresupuestoDTO dto)
        {
            var existente = await _context.Presupuestos
           .FirstOrDefaultAsync(p =>
               p.TipoGastoId == dto.TipoGastoId &&
               p.Mes == dto.Mes &&
               p.Anio == dto.Anio);

            if (existente != null)
            {
                existente.Monto = dto.Monto;
            }
            else
            {
                var nuevo = new Presupuesto
                {
                    TipoGastoId = dto.TipoGastoId,
                    Mes = dto.Mes,
                    Anio = dto.Anio,
                    Monto = dto.Monto
                };

                _context.Presupuestos.Add(nuevo);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<PresupuestoDTO?> ObtenerPresupuestoAsync(int tipoGastoId, int mes, int anio)
        {
            return await _context.Presupuestos
           .Where(p => p.TipoGastoId == tipoGastoId && p.Mes == mes && p.Anio == anio)
           .Select(p => new PresupuestoDTO
           {
               TipoGastoId = p.TipoGastoId,
               NombreTipoGasto = p.TipoGasto.Nombre,
               Mes = p.Mes,
               Anio = p.Anio,
               Monto = p.Monto
           })
           .FirstOrDefaultAsync();
        }

        public async Task<List<PresupuestoDTO>> ObtenerTodosAsync()
        {
            return await _context.Presupuestos
           .Select(p => new PresupuestoDTO
           {
               TipoGastoId = p.TipoGastoId,
               NombreTipoGasto = p.TipoGasto.Nombre,
               Mes = p.Mes,
               Anio = p.Anio,
               Monto = p.Monto
           }).ToListAsync();
        }
    }
}
