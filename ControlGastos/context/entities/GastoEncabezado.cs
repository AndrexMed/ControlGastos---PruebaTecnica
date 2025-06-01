using System;
using System.Collections.Generic;

namespace ControlGastos.context.entities;

public partial class GastoEncabezado
{
    public int GastoEncabezadoId { get; set; }

    public DateTime Fecha { get; set; }

    public int FondoMonetarioId { get; set; }

    public string? Observaciones { get; set; }

    public string? NombreComercio { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public virtual FondoMonetario FondoMonetario { get; set; } = null!;

    public virtual ICollection<GastoDetalle> GastoDetalles { get; set; } = new List<GastoDetalle>();
}
