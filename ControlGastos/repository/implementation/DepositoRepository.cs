using ControlGastos.context;
using ControlGastos.context.dtos;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;

namespace ControlGastos.repository.implementation
{
    public class DepositoRepository(ControlGastosContext context) : IDepositoRepository
    {
        private readonly ControlGastosContext _context = context;
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
