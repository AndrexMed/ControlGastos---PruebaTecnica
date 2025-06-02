using ControlGastos.context;
using ControlGastos.context.dtos;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.repository.implementation
{
    public class GastoRepository(ControlGastosContext context) : IGastoRepository
    {
        private readonly ControlGastosContext _context = context;
        public async Task<IEnumerable<GastoMovimientoDTO>> ObtenerGastosAsync(DateTime desde, DateTime hasta)
        {
            var hastaFin = hasta.Date.AddDays(1).AddTicks(-1);

            var gastos = await _context.GastoEncabezados
                .Where(g => g.Fecha >= desde && g.Fecha <= hastaFin)
                .Select(g => new GastoMovimientoDTO
                {
                    Fecha = g.Fecha,
                    FondoMonetario = g.FondoMonetario.Nombre,
                    MontoTotal = g.GastoDetalles.Sum(d => d.Monto),
                    Observaciones = g.Observaciones ?? string.Empty,
                    NombreComercio = g.NombreComercio ?? string.Empty,
                    TipoDocumento = g.TipoDocumento ?? string.Empty,
                    GastoDetalleDTOs = g.GastoDetalles
                    .Select(d => new GastoDetalleDTO
                    {
                        TipoGastoId = d.TipoGastoId,
                        Monto = d.Monto
                    }).ToList()
                }).ToListAsync();

            return gastos;
        }

        public async Task<GastoResultadoDTO> RegistrarGastoAsync(GastoRegistroDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var encabezado = new GastoEncabezado
            {
                Fecha = dto.Fecha,
                FondoMonetarioId = dto.FondoMonetarioId,
                Observaciones = dto.Observaciones,
                NombreComercio = dto.NombreComercio,
                TipoDocumento = dto.TipoDocumento
            };

            // Validación de presupuesto ANTES de guardar detalles
            var anio = dto.Fecha.Year;
            var mes = dto.Fecha.Month;
            var alertas = new List<SobregiroDTO>();

            foreach (var grupo in dto.Detalles.GroupBy(d => d.TipoGastoId))
            {
                var gastoActual = await _context.GastoDetalles
                    .Include(g => g.GastoEncabezado)
                    .Where(g => g.TipoGastoId == grupo.Key &&
                                g.GastoEncabezado.Fecha.Month == mes &&
                                g.GastoEncabezado.Fecha.Year == anio)
                    .SumAsync(g => g.Monto);

                var presupuestado = await _context.Presupuestos
                    .Where(p => p.TipoGastoId == grupo.Key && p.Mes == mes && p.Anio == anio)
                    .Select(p => p.Monto)
                    .FirstOrDefaultAsync();

                var totalConNuevo = gastoActual + grupo.Sum(g => g.Monto);
                if (totalConNuevo > presupuestado)
                {
                    alertas.Add(new SobregiroDTO
                    {
                        TipoGastoId = grupo.Key,
                        Presupuesto = presupuestado,
                        Ejecutado = totalConNuevo
                    });
                }
            }

            // Guardar encabezado y detalles después de validación
            _context.GastoEncabezados.Add(encabezado);
            await _context.SaveChangesAsync();

            var detalles = dto.Detalles.Select(d => new GastoDetalle
            {
                GastoEncabezadoId = encabezado.GastoEncabezadoId,
                TipoGastoId = d.TipoGastoId,
                Monto = d.Monto
            }).ToList();

            _context.GastoDetalles.AddRange(detalles);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new GastoResultadoDTO
            {
                GastoEncabezadoId = encabezado.GastoEncabezadoId,
                TieneSobregiro = alertas.Any(),
                Alertas = alertas
            };
        }

    }
}
