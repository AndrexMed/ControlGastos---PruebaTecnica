using ControlGastos.context;
using ControlGastos.context.entities;
using ControlGastos.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.repository.implementation
{
    public class FondoMonetarioRepository(ControlGastosContext context) : IFondoMonetarioRepository
    {
        private readonly ControlGastosContext _context = context;

        public async Task<FondoMonetario> AddAsync(FondoMonetario fondo)
        {
            _context.FondoMonetarios.Add(fondo);
            await _context.SaveChangesAsync();
            return fondo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fondo = await _context.FondoMonetarios.FindAsync(id);
            if (fondo == null) return false;

            _context.FondoMonetarios.Remove(fondo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FondoMonetario>> GetAllAsync()
        {
            return await _context.FondoMonetarios.ToListAsync();
        }

        public async Task<FondoMonetario?> GetByIdAsync(int id)
        {
            return await _context.FondoMonetarios.FindAsync(id);
        }

        public async Task<FondoMonetario?> UpdateAsync(FondoMonetario fondo)
        {
            var existente = await _context.FondoMonetarios.FindAsync(fondo.FondoMonetarioId);
            if (existente == null) return null;

            existente.Nombre = fondo.Nombre;
            existente.TipoFondo = fondo.TipoFondo;
            existente.NumeroCuenta = fondo.NumeroCuenta;

            await _context.SaveChangesAsync();
            return existente;
        }
    }
}
