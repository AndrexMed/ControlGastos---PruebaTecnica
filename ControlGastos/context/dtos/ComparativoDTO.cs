namespace ControlGastos.context.dtos
{
    public class ComparativoDTO
    {
        public int TipoGastoId { get; set; }
        public string NombreTipoGasto { get; set; } = string.Empty;
        public decimal Presupuesto { get; set; }
        public decimal Ejecutado { get; set; }
    }
}
