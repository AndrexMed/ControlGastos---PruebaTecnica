namespace ControlGastos.context.dtos
{
    public class GastoResultadoDTO
    {
        public int GastoEncabezadoId { get; set; }
        public bool TieneSobregiro { get; set; }
        public List<SobregiroDTO> Alertas { get; set; } = new();
    }
}
