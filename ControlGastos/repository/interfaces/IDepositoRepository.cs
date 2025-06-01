using ControlGastos.context.dtos;

namespace ControlGastos.repository.interfaces
{
    public interface IDepositoRepository
    {
        Task<int> RegistrarDepositoAsync(DepositoRegistroDTO dto);
    }
}
