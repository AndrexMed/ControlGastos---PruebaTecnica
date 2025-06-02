using ControlGastos.context;
using ControlGastos.context.dtos;
using ControlGastos.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.repository.implementation
{
    public class GraficosRepository(ControlGastosContext context) : IGraficosRepository
    {
        private readonly ControlGastosContext _context = context;

        public async Task<List<ComparativoDTO>> ObtenerComparativoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            // 1. Obtener presupuestos SIN filtrar por fecha aún
            var presupuestos = await _context.Presupuestos.ToListAsync();

            // 2. Filtrar en memoria por rango de fechas
            var presupuestosFiltrados = presupuestos
                .Where(p =>
                    new DateTime(p.Anio, p.Mes, 1) >= new DateTime(fechaInicio.Year, fechaInicio.Month, 1) &&
                    new DateTime(p.Anio, p.Mes, 1) <= new DateTime(fechaFin.Year, fechaFin.Month, 1))
                .ToList();

            // 3. Obtener gastos ejecutados directamente desde la base de datos (esta parte sí es traducible)
            var gastos = await _context.GastoDetalles
                .Include(g => g.GastoEncabezado)
                .Where(d => d.GastoEncabezado.Fecha >= fechaInicio && d.GastoEncabezado.Fecha <= fechaFin)
                .ToListAsync();

            // 4. Obtener tipos de gasto
            var tipos = await _context.TipoGastos.ToListAsync();

            // 5. Armar el comparativo
            var comparativo = tipos.Select(tipo => new ComparativoDTO
            {
                TipoGastoId = tipo.TipoGastoId,
                NombreTipoGasto = tipo.Nombre,
                Presupuesto = presupuestosFiltrados
                    .Where(p => p.TipoGastoId == tipo.TipoGastoId)
                    .Sum(p => p.Monto),
                Ejecutado = gastos
                    .Where(g => g.TipoGastoId == tipo.TipoGastoId)
                    .Sum(g => g.Monto)
            }).ToList();

            return comparativo;
        }


    }
}
