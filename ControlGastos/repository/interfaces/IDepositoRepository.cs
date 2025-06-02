using ControlGastos.context.dtos;

namespace ControlGastos.repository.interfaces
{
    public interface IDepositoRepository
    {
        Task<int> RegistrarDepositoAsync(DepositoRegistroDTO dto);
        Task<List<DepositoRegistroDTO>> ObtenerDepositosAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
