using Examen_jose_lira.DTOs;
namespace Examen_jose_lira.DTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }

        public string? NumeroFactura { get; set; }

        public DateTime? FechaHora { get; set; }

        public ClienteDTO? Cliente { get; set; }

        public List<DetalleFacturaDTO>? DetallesFactura { get; set; }
    }
}
