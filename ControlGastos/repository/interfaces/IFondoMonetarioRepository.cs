using ControlGastos.context.entities;

namespace ControlGastos.repository.interfaces
{
    public interface IFondoMonetarioRepository
    {
        Task<IEnumerable<FondoMonetario>> GetAllAsync();
        Task<FondoMonetario?> GetByIdAsync(int id);
        Task<FondoMonetario> AddAsync(FondoMonetario fondo);
        Task<FondoMonetario?> UpdateAsync(FondoMonetario fondo);
        Task<bool> DeleteAsync(int id);
    }
}
