namespace ControlGastos.context.dtos
{
    public class GastoRegistroDTO
    {
        public DateTime Fecha { get; set; }
        public int FondoMonetarioId { get; set; }
        public string? Observaciones { get; set; }
        public string? NombreComercio { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public List<GastoDetalleDTO> Detalles { get; set; } = new();
    }
}
