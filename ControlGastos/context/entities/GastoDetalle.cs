using System;
using System.Collections.Generic;

namespace ControlGastos.context.entities;

public partial class GastoDetalle
{
    public int GastoDetalleId { get; set; }

    public int GastoEncabezadoId { get; set; }

    public int TipoGastoId { get; set; }

    public decimal Monto { get; set; }

    public virtual GastoEncabezado GastoEncabezado { get; set; } = null!;

    public virtual TipoGasto TipoGasto { get; set; } = null!;
}
