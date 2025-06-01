using System;
using System.Collections.Generic;

namespace ControlGastos.context.entities;

public partial class FondoMonetario
{
    public int FondoMonetarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoFondo { get; set; } = null!;

    public string? NumeroCuenta { get; set; }

    public virtual ICollection<Deposito> Depositos { get; set; } = new List<Deposito>();

    public virtual ICollection<GastoEncabezado> GastoEncabezados { get; set; } = new List<GastoEncabezado>();
}
