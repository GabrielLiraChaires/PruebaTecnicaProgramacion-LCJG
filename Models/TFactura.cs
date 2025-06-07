using System;
using System.Collections.Generic;

namespace Examen_jose_lira.Models;

public partial class TFactura
{
    public int IdFactura { get; set; }

    public string? NumeroFactura { get; set; }

    public DateTime? FechaHora { get; set; }

    public int? IdCliente { get; set; }

    public virtual TCliente? IdClienteNavigation { get; set; }

    public virtual ICollection<TDetalleFactura> TDetalleFacturas { get; set; } = new List<TDetalleFactura>();
}
