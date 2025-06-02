namespace ControlGastos.context.dtos
{
    public class PresupuestoDTO
    {
        public int TipoGastoId { get; set; }
        public string? NombreTipoGasto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal Monto { get; set; }
    }
}
