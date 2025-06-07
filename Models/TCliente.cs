using System;
using System.Collections.Generic;

namespace Examen_jose_lira.Models;

public partial class TCliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Domicilio { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<TFactura> TFacturas { get; set; } = new List<TFactura>();
}
