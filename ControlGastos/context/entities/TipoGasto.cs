using System;
using System.Collections.Generic;

namespace ControlGastos.context.entities;

public partial class TipoGasto
{
    public int TipoGastoId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<GastoDetalle> GastoDetalles { get; set; } = new List<GastoDetalle>();

    public virtual ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
}
