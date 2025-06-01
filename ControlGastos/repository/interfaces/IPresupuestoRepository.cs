using ControlGastos.context.dtos;

namespace ControlGastos.repository.interfaces
{
    public interface IPresupuestoRepository
    {
        Task CrearOActualizarPresupuestoAsync(PresupuestoDTO dto);
        Task<PresupuestoDTO?> ObtenerPresupuestoAsync(int tipoGastoId, int mes, int anio);
        Task<List<PresupuestoDTO>> ObtenerTodosAsync();
    }
}
