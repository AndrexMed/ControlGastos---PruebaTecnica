namespace ControlGastos.context.dtos
{
    public class DepositoRegistroDTO
    {
        public DateTime Fecha { get; set; }
        public int FondoMonetarioId { get; set; }
        public string? NombreFondoMonetario { get; set; }
        public decimal Monto { get; set; }
    }
}
