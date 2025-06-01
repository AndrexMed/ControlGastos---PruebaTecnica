using ControlGastos.context.entities;

namespace ControlGastos.repository.interfaces
{
    public interface ITipoGastoRepository
    {
        Task<IEnumerable<TipoGasto>> GetAllAsync();
        Task<TipoGasto?> GetByIdAsync(int id);
        Task<TipoGasto> CreateAsync(TipoGasto tipoGasto);
        Task UpdateAsync(TipoGasto tipoGasto);
        Task DeleteAsync(int id);
    }
}
