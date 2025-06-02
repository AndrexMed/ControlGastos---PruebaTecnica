using ControlGastos.context;
using ControlGastos.context.dtos;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.repository.implementation
{
    public class DepositoRepository(ControlGastosContext context) : IDepositoRepository
    {
        private readonly ControlGastosContext _context = context;

        public Task<List<DepositoRegistroDTO>> ObtenerDepositosAsync(DateTime fechaInicio, DateTime fechaFin)
        {

            return _context.Depositos
                .Where(d => d.Fecha >= fechaInicio && d.Fecha <= fechaFin)
                .Select(d => new DepositoRegistroDTO
                {
                    Fecha = d.Fecha,
                    FondoMonetarioId = d.FondoMonetarioId,
                    NombreFondoMonetario = d.FondoMonetario.Nombre,
                    Monto = d.Monto
                })
                .ToListAsync();
        }

        public async Task<int> RegistrarDepositoAsync(DepositoRegistroDTO dto)
        {
            var deposito = new Deposito
            {
                Fecha = dto.Fecha,
                FondoMonetarioId = dto.FondoMonetarioId,
                Monto = dto.Monto
            };

            _context.Depositos.Add(deposito);
            await _context.SaveChangesAsync();

            return deposito.DepositoId;
        }
    }
}
