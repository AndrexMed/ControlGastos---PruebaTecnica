namespace ControlGastos.context.dtos
{
    public class GastoMovimientoDTO
    {
        public DateTime Fecha { get; set; }
        public string FondoMonetario { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
        public string NombreComercio { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public required List<GastoDetalleDTO> GastoDetalleDTOs { get; set; } 
    }
}
