using System;
using System.Collections.Generic;

namespace Examen_jose_lira.Models;

public partial class TProducto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Marca { get; set; }

    public decimal? Costo { get; set; }

    public decimal? PrecioVenta { get; set; }

    public virtual ICollection<TDetalleFactura> TDetalleFacturas { get; set; } = new List<TDetalleFactura>();
}
