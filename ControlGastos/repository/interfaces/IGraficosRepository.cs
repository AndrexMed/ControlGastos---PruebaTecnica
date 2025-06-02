using ControlGastos.context.dtos;

namespace ControlGastos.repository.interfaces
{
    public interface IGraficosRepository
    {
        Task<List<ComparativoDTO>> ObtenerComparativoAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
