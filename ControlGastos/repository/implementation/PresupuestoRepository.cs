using ControlGastos.context;
using ControlGastos.context.dtos;
using ControlGastos.repository.interfaces;

namespace ControlGastos.repository.implementation
{
    public class PresupuestoRepository(ControlGastosContext context) : IPresupuestoRepository
    {
        private readonly ControlGastosContext _context = context;

        public Task CrearOActualizarPresupuestoAsync(PresupuestoDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<PresupuestoDTO?> ObtenerPresupuestoAsync(int tipoGastoId, int mes, int anio)
        {
            throw new NotImplementedException();
        }

        public Task<List<PresupuestoDTO>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
