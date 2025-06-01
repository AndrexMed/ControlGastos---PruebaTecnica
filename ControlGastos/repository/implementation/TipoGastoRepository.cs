using ControlGastos.context;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.repository.implementation
{
    public class TipoGastoRepository(ControlGastosContext context) : ITipoGastoRepository
    {
        private readonly ControlGastosContext _context = context;

        public async Task<TipoGasto> CreateAsync(TipoGasto tipoGasto)
        {
            // Código generado automático
            int ultimoId = await _context.TipoGastos.CountAsync() + 1;
            tipoGasto.Codigo = $"TG{ultimoId.ToString("D4")}";

            _context.TipoGastos.Add(tipoGasto);
            await _context.SaveChangesAsync();
            return tipoGasto;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TipoGastos.FindAsync(id);
            if (item != null)
            {
                _context.TipoGastos.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TipoGasto>> GetAllAsync()
        {
            return await _context.TipoGastos.ToListAsync();
        }

        public async Task<TipoGasto?> GetByIdAsync(int id)
        {
            return await _context.TipoGastos.FindAsync(id);
        }

        public async Task UpdateAsync(TipoGasto tipoGasto)
        {
            _context.TipoGastos.Update(tipoGasto);
            await _context.SaveChangesAsync();
        }
    }
}
