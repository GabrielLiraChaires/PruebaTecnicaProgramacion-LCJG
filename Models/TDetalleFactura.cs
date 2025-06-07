using System;
using System.Collections.Generic;

namespace Examen_jose_lira.Models;

public partial class TDetalleFactura
{
    public int IdDetalle { get; set; }

    public int? IdFactura { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public virtual TFactura? IdFacturaNavigation { get; set; }

    public virtual TProducto? IdProductoNavigation { get; set; }
}
