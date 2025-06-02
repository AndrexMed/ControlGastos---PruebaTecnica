using ControlGastos.context.dtos;

namespace ControlGastos.repository.interfaces
{
    public interface IGastoRepository
    {
        Task<GastoResultadoDTO> RegistrarGastoAsync(GastoRegistroDTO dto);
        Task<IEnumerable<GastoMovimientoDTO>> ObtenerGastosAsync(DateTime desde, DateTime hasta);
    }
}
