using System;
using System.Collections.Generic;

namespace ControlGastos.context.entities;

public partial class Presupuesto
{
    public int PresupuestoId { get; set; }

    public int Mes { get; set; }

    public int Anio { get; set; }

    public int TipoGastoId { get; set; }

    public decimal Monto { get; set; }

    public virtual TipoGasto TipoGasto { get; set; } = null!;
}
