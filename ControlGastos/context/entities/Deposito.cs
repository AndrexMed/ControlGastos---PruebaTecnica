using System;
using System.Collections.Generic;

namespace ControlGastos.context.entities;

public partial class Deposito
{
    public int DepositoId { get; set; }

    public DateTime Fecha { get; set; }

    public int FondoMonetarioId { get; set; }

    public decimal Monto { get; set; }

    public virtual FondoMonetario FondoMonetario { get; set; } = null!;
}
